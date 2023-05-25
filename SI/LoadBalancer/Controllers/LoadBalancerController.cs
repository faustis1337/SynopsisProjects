using Common;
using LoadBalancer.LoadBalancer;
using LoadBalancer.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
namespace LoadBalancer.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class LoadBalancerController : Controller
    {
        private readonly ILoadBalancer _loadBalancer;

        public LoadBalancerController(ILoadBalancer loadBalancer)
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
        
        [HttpGet("GetServers")]
        public IActionResult  GetServers()
        {
            return Ok(_loadBalancer.GetAllServices());
        }
        
        [HttpPost("AddServer")]
        public IActionResult  AddServer(string url)
        {
            return Ok(_loadBalancer.AddService(url));
        }

        [HttpGet]
        public IActionResult Search(string terms, int numberOfResults)
        {
            ServiceModel service = _loadBalancer.NextService();
            service.activeQueries += 1;

            RestClient serviceClient = new(service.url);
            RestRequest request = new("/Search");
            request.AddParameter("terms", terms);
            request.AddParameter("numberOfResults", numberOfResults);
            Task<SearchResult?> response = serviceClient.GetAsync<SearchResult>(request);
            response.Wait();
            SearchResult? result = response.Result;
            if (result is null)
            {
                return NotFound("Search failed lmao");
            }
            Console.WriteLine($"Returned {result.Documents.Count} results.");
            Console.WriteLine($"Time taken: {result.ElapsedMlliseconds}ms.");
            service.activeQueries -= 1;
            service.currentLatency = result.ElapsedMlliseconds;
            return Ok(result);
        }
    }
}
