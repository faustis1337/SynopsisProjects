using Microsoft.AspNetCore.Mvc;
using SenderAPI.Models;

namespace SenderAPI.Controllers;

public class SenderController : Controller
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Messages messages)
    {
        
        
        return null;
    }
}