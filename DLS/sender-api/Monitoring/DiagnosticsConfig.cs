using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace SenderAPI.Monitoring;

public static class DiagnosticsConfig
{
    public const string ServiceName = "SenderAPI";
    public const string ServiceVersion = "1.0.0";
    public static readonly ActivitySource ActivitySource = new ActivitySource(ServiceName, ServiceVersion);

    private static readonly Meter Meter = new(ServiceName);
    public static Counter<long> RequestCounter =
        Meter.CreateCounter<long>("app.request_counter");

}