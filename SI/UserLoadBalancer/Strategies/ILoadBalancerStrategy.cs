using UserLoadBalancer.Models;

namespace UserLoadBalancer.Strategies;

public interface ILoadBalancerStrategy
{
    public ServiceModel NextService(List<ServiceModel> services);
}