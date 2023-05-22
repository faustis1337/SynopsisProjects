using Microsoft.AspNetCore.Mvc;
using UserApi.Data;
using UserApi.Models;

namespace UserApi.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private IUserRepository _userRepository;
    
    public UserController(UserApiContext userApiContext, IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    [HttpGet]
    public async Task<IActionResult> Get(string username, string password)
    {
        User? user = await _userRepository.GetUser(username,password);

        if (user == null)
        {
            return NotFound();
        }
        
        return new ObjectResult(user);
    }

}