namespace UserLoadBalancer.Models;

public class ServiceModel {
    public string url { get; set; }
    public double currentLatency { get; set; }
    public int activeQueries { get; set; }
}