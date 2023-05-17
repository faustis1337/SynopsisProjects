using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using SenderAPI.Monitoring;

    
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddOpenTelemetry()
    // Configure tracing
    .WithTracing(tracerProviderBuilder => 
        tracerProviderBuilder
            .AddOtlpExporter()
            .AddConsoleExporter()
            .AddSource(DiagnosticsConfig.ActivitySource.Name,DiagnosticsConfig.ActivitySource.Version)
            .ConfigureResource(resource => 
                resource.AddService(DiagnosticsConfig.ServiceName))
            .AddAspNetCoreInstrumentation()
            .Build()
    ) 
    // Configure metrics
    .WithMetrics(metricsProviderBuilder =>
        metricsProviderBuilder
            .AddOtlpExporter()
            .AddConsoleExporter()
            .ConfigureResource(resource => 
                resource.AddService(DiagnosticsConfig.ServiceName))
            .AddAspNetCoreInstrumentation()
            .Build()
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

// Configure the HTTP request pipeline
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();