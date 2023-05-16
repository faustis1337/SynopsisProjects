using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SenderAPI.Models;

namespace SenderAPI.Controllers;

public class SenderController : Controller
{
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Messages messages)
    {
        RestClient serviceClient = new("url goes here");
        RestRequest request = new("/Post");
        request.AddBody(new
        {
            name = "Message via restsharp"
        });
        request.AddParameter("Id", messages.Id);
        request.AddParameter("Message", messages.Message);
        //var response = await serviceClient.ExecuteAsync(request);
        
        return Ok();
    }
}