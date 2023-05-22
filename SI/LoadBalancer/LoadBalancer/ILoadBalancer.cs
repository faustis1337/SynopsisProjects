using LoadBalancer.Models;
using LoadBalancer.Strategies;

namespace LoadBalancer.LoadBalancer;

public interface ILoadBalancer
{
    public List<ServiceModel> GetAllServices();
    public int AddService(string url);
    public int RemoveService(int id);
    public ILoadBalancerStrategy GetActiveStrategy();
    public void SetActiveStrategy(ILoadBalancerStrategy strategy);
    public ServiceModel NextService();
}