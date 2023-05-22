using UserLoadBalancer.Models;

namespace UserLoadBalancer.Strategies;

public class BasicStrategy : ILoadBalancerStrategy
{
    public ServiceModel NextService(List<ServiceModel> services)
    {
        return services.First();
    }
}