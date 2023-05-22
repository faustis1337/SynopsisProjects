using LoadBalancer.Models;

namespace LoadBalancer.Strategies;

public class BasicStrategy : ILoadBalancerStrategy
{
    public ServiceModel NextService(List<ServiceModel> services)
    {
        return services.First();
    }
}