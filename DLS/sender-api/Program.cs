using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenTelemetryTracing(b => b
    .SetResourceBuilder(ResourceBuilder.CreateDefault()
        .AddService("SenderAPI"))
    .AddAspNetCoreInstrumentation()
    .AddHttpClientInstrumentation()
    .AddMyConsoleExporter()
);

// Add support for tracing
builder.Services.AddOpenTelemetryTracing(b => b
    .SetResourceBuilder(ResourceBuilder
        .CreateDefault().AddService("SenderAPI"))
    .AddAspNetCoreInstrumentation()
    .AddHttpClientInstrumentation()
    .AddConsoleExporter()
);

// Add support for metrics
builder.Services.AddOpenTelemetryMetrics(b => b
    .SetResourceBuilder(ResourceBuilder
        .CreateDefault().AddService("SenderAPI"))
    .AddAspNetCoreInstrumentation()
    .AddConsoleExporter()
);

// Configure logging
builder.Logging.AddOpenTelemetry(options =>
    {
        options.IncludeFormattedMessage = true;
        options.SetResourceBuilder(ResourceBuilder.CreateDefault()
            .AddService("SenderAPI"));
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