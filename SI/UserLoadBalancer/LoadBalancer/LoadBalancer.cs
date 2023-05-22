using UserLoadBalancer.Models;
using UserLoadBalancer.Strategies;

namespace UserLoadBalancer.LoadBalancer;

public class LoadBalancer : ILoadBalancer
{
    private ILoadBalancerStrategy _strategy;
    private readonly List<ServiceModel> _services;

    public LoadBalancer()
    { 
        //Activate a strategy by commenting out the one you dont want to use
        //and removing comments from the one you want to use. 
        
        //_strategy = new BasicStrategy();
        //_strategy = new LeastQueriesStrategy();
        _strategy = new RoundRobinStrategy();
        
        _services = new List<ServiceModel>();
    }

    public List<ServiceModel> GetAllServices()
    {
        return _services;
    }

    public int AddService(string url)
    {
        _services.Add(new ServiceModel{url = url});
        return _services.Count - 1;
    }
        
    public int RemoveService(int id)
    {
        _services.RemoveAt(id);
        return id;
    }
        
    public ILoadBalancerStrategy GetActiveStrategy()
    {
        return _strategy; 
    }

    public void SetActiveStrategy(ILoadBalancerStrategy strategy)
    {
        _strategy = strategy;
    }

    public ServiceModel NextService()
    {
        return _strategy.NextService(_services);
    }
}