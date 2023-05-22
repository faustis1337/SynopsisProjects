using UserLoadBalancer.Models;

namespace UserLoadBalancer.Strategies;

public class RoundRobinStrategy : ILoadBalancerStrategy
{
    private int _count;

    
    public ServiceModel NextService(List<ServiceModel> services)
    {
        if(services.Count > 0 && _count < services.Count)
        {
            var serviceToUse = services[_count];
            
            _count += 1;
            if (_count == services.Count) _count = 0;
            Console.WriteLine($"Returned service url:{serviceToUse.url} with service index: {_count}");
            return serviceToUse;
        }
        throw new IndexOutOfRangeException();
    }
}