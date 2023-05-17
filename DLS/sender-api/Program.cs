using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using SenderAPI.Monitoring;
using OpenTelemetry.Exporter.Jaeger; 

    
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddOpenTelemetry()
    // Configure tracing
    .WithTracing(tracerProviderBuilder => 
        tracerProviderBuilder
            .AddSource(DiagnosticsConfig.ActivitySource.Name)
            .ConfigureResource(resource => resource
                .AddService(DiagnosticsConfig.ServiceName))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            //.AddJaegerExporter()
            .AddOtlpExporter()
            ) 
    // Configure metrics
    .WithMetrics(metricsProviderBuilder =>
        metricsProviderBuilder
            .ConfigureResource(resource => resource
                .AddService(DiagnosticsConfig.ServiceName))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddOtlpExporter()
            //.AddJaegerExporter() //cant add JaegerExporter to the other metrics
    );

// Configure logging
builder.Logging.AddOpenTelemetry(options =>
    {
        options.IncludeFormattedMessage = true;
        options.SetResourceBuilder(ResourceBuilder.CreateDefault()
            .AddService(DiagnosticsConfig.ServiceName));
        options.AddConsoleExporter()
            //.AddJaegerExporter()
            ;
    });


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

// Configure the HTTP request pipeline
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();