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
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MessageEntity messageEntity)
    {
        using var myActivity = DiagnosticsConfig.ActivitySource.StartActivity("SenderApi");
        
            var apiUrl = "http://host.docker.internal:8001";
            RestClient client = new RestClient(apiUrl);
            RestRequest request = new RestRequest("/api/messages", Method.Post);
            
            myActivity?.SetTag("id", messageEntity.Id);
            myActivity?.SetTag("message", messageEntity.Message);
            
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

// var activity = myActivity?.Context ?? System.Diagnostics.Activity.Current?.Context ?? default;
// var propagationContext = new PropagationContext(activity, Baggage.Current);
// var propagator = new TraceContextPropagator();
// propagator.Inject(propagationContext, messageEntity, (r, key, value) =>
// {
//     r.Headers.Add(key, value);
// });
