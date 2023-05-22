using UserLoadBalancer.Strategies;

namespace UserLoadBalancer.Models;

public enum Strategy
{
    RoundRobin,
    LeastConnections
}

static class StrategyExtensions 
{
    public static ILoadBalancerStrategy From(this Strategy strategy) 
    {
        switch (strategy) 
        {
            case Strategy.RoundRobin:   return new RoundRobinStrategy();
            case Strategy.LeastConnections:  return new LeastQueriesStrategy();
            default: return new BasicStrategy(); //You chose poorly therefore you get the stinky slow one
        }
    }
}