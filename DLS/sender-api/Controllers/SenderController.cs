using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry;
using OpenTelemetry.Context.Propagation;
using RestSharp;
using SenderAPI.Models;
using SenderAPI.Monitoring;

namespace SenderAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SenderController : Controller
{
    private static readonly ActivitySource Activity = new(nameof(SenderController));
    
    private readonly ILogger<SenderController> _logger;
    private readonly IConfiguration _configuration;

    public SenderController(
        ILogger<SenderController> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MessageEntity messageEntity)
    {
        using (var myActivity = Activity.StartActivity("SenderApi", ActivityKind.Client, parentContext: new ActivityContext()))
        {

            myActivity?.SetTag("id", messageEntity.Id);
            myActivity?.SetTag("message", messageEntity.Message);

            var activity = myActivity?.Context ?? System.Diagnostics.Activity.Current?.Context ?? default;
            var propagationContext = new PropagationContext(activity, Baggage.Current);
            var propagator = new TraceContextPropagator();
            propagator.Inject(propagationContext, messageEntity, (r, key, value) =>
            {
                r.Headers.Add(key, value);
            });
            
            
            var apiUrl = "http://host.docker.internal:8001";
            RestClient client = new RestClient(apiUrl);
            RestRequest request = new RestRequest("/api/messages", Method.Post);

            request.AddJsonBody(new
            {
                id = messageEntity.Id,
                message = messageEntity.Message
            });

            var response = await client.ExecutePostAsync(request);

            if (!response.IsSuccessful)
            {
                return BadRequest(response.StatusCode);
            }

            return Ok(response.Content);
        }
    }
}
