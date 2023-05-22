using Common;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using UserLoadBalancer.LoadBalancer;
using UserLoadBalancer.Models;

namespace UserLoadBalancer.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class UserLoadBalancerController : Controller
    {
        private readonly ILoadBalancer _loadBalancer;

        public UserLoadBalancerController(ILoadBalancer loadBalancer)
        {
            _loadBalancer = loadBalancer;
        }

        [HttpPost]
        public void Create(string url)
        {
            _loadBalancer.AddService(url);
            Console.WriteLine("added service: " + url);
        }

        [HttpPut("SetStrategy")]
        public void SetStrategy(Strategy strategy)
        {
            _loadBalancer.SetActiveStrategy(strategy.From());
            Console.WriteLine($"Set strategy to {strategy.ToString()}");
        }

        [HttpGet]
        [ActionName("GetSearchResult")]
        public IActionResult Login(string username, string password)
        {
            ServiceModel service = _loadBalancer.NextService();
            service.activeQueries += 1;

            RestClient serviceClient = new(service.url);
            RestRequest request = new("/User");
            request.AddParameter("username",username);
            request.AddParameter("password", password);
            Task<User?> response = serviceClient.GetAsync<User>(request);
            response.Wait();
            User? result = response.Result;
            if (result.id == 0)
            {
                result = null;
            }
            
            if (result is null)
            {
                return NotFound("User doesnt exist");
            }
            service.activeQueries -= 1;
            return Ok(result);
        }
    }
}
