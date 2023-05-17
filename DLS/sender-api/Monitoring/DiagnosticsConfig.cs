using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace SenderAPI.Monitoring;

public static class DiagnosticsConfig
{
    public const string ServiceName = "SenderAPI";
    public static readonly ActivitySource ActivitySource = new ActivitySource(ServiceName);

    private static readonly Meter Meter = new(ServiceName);
    public static Counter<long> RequestCounter =
        Meter.CreateCounter<long>("app.request_counter");

}