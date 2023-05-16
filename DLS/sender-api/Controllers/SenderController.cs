using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SenderAPI.Models;

namespace SenderAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SenderController : Controller
{
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MessageEntity messageEntity)
    {
        var apiUrl = "http://host.docker.internal:8001";
        RestClient client = new RestClient(apiUrl);
        RestRequest request = new RestRequest("/api/messages",Method.Post);

        request.AddJsonBody(new
        {
            id = messageEntity.Id,
            message = messageEntity.Message
        });

        var response = await client.ExecutePostAsync(request);

        if (response.IsSuccessful)
        {
            return Ok(response.Content);
        }
        else
        {
            Console.WriteLine("Error: " + response.StatusCode);
        }
        
        return Ok();
    }
}