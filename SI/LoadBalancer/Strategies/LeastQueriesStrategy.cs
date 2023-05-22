using LoadBalancer.Models;

namespace LoadBalancer.Strategies;

public class LeastQueriesStrategy : ILoadBalancerStrategy
{
    //Takes the service with the least amount of active queries
    public ServiceModel NextService(List<ServiceModel> services) {
        // Order all services by amount of active queries (connections). Ascending
        IOrderedEnumerable<ServiceModel> orderedServices = services.OrderBy(x => x.activeQueries);
        // Get the service with the least queries
        var service = orderedServices.First();
        Console.WriteLine($"Returned service url:{service.url} with {service.activeQueries} active queries");
        return service;
    }
}