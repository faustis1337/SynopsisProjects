using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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
        myActivity?.SetTag("id", messageEntity.Id);
        myActivity?.SetTag("message", messageEntity.Message);
        
        var apiUrl = "http://host.docker.internal:8001";
        RestClient client = new RestClient(apiUrl);
        RestRequest request = new RestRequest("/api/messages",Method.Post);

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