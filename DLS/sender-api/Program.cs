using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Prometheus;
using SenderAPI.Monitoring;

    
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenTelemetry()
    // Configure tracing
    .WithTracing(b =>
        b
            //.AddConsoleExporter()
            //.AddOtlpExporter()
            .AddSource(DiagnosticsConfig.ActivitySource.Name, DiagnosticsConfig.ActivitySource.Version)
            .ConfigureResource(resource =>
                resource.AddService(DiagnosticsConfig.ServiceName))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddJaegerExporter()
    )
    // Configure metrics
    .WithMetrics(b =>
        b
            .AddOtlpExporter()
            .AddConsoleExporter()
            .ConfigureResource(resource => 
                resource.AddService(DiagnosticsConfig.ServiceName))
            .AddAspNetCoreInstrumentation()
    );

// Configure logging
builder.Logging.AddOpenTelemetry(options =>
    {
        options.IncludeFormattedMessage = true;
        options.SetResourceBuilder(ResourceBuilder.CreateDefault()
            .AddService(DiagnosticsConfig.ServiceName));
        options.AddConsoleExporter();
    });


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

app.UseHttpMetrics();
app.MapMetrics();

// Configure the HTTP request pipeline
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();