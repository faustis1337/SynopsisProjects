using LoadBalancer.LoadBalancer;
using LoadBalancer.Models;

namespace LoadBalancer.Strategies;

public interface ILoadBalancerStrategy
{
    public ServiceModel NextService(List<ServiceModel> services);
}