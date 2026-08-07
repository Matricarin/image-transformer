using ImageTransformer.Apis;
using ImageTransformer.Services;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder();

builder.Services.AddImageTransformServices();

builder.Services.AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics.AddAspNetCoreInstrumentation()
            .AddRuntimeInstrumentation()
            .AddMeter(ApplicationDiagnostics.ServiceName)
            .AddPrometheusExporter();
    });

var app = builder.Build();

app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.UseHttpsRedirection();

app.MapImageTransformApi();

app.Run();