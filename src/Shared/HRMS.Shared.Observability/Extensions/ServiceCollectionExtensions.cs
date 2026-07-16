using HRMS.Shared.Observability.Enrichers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Events;

namespace HRMS.Shared.Observability.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddObservability(
        this IServiceCollection services,
        string serviceName,
        string serviceVersion,
        string? jaegerEndpoint = null,
        Action<LoggerConfiguration>? configureSerilog = null)
    {
        services.AddHttpContextAccessor();

        var loggerConfig = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .MinimumLevel.Override("MassTransit", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("ServiceName", serviceName)
            .Enrich.WithProperty("ServiceVersion", serviceVersion)
            .Enrich.With<TenantIdEnricher>()
            .Enrich.With<UserIdEnricher>()
            .Enrich.With<CorrelationIdEnricher>()
            .WriteTo.Console(
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level:u3}] [{CorrelationId}] [{TenantId}] [{UserId}] {Message:lj}{NewLine}{Exception}");

        configureSerilog?.Invoke(loggerConfig);

        Log.Logger = loggerConfig.CreateLogger();

        services.AddSerilog(Log.Logger);

        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource
                .AddService(serviceName: serviceName, serviceVersion: serviceVersion)
                .AddAttributes(new Dictionary<string, object>
                {
                    ["deployment.environment"] = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"
                }))
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation(options =>
                    {
                        options.RecordException = true;
                        options.Filter = httpContext =>
                        {
                            var path = httpContext.Request.Path.Value ?? "";
                            return !path.StartsWith("/health")
                                && !path.StartsWith("/ready")
                                && !path.StartsWith("/metrics")
                                && !path.StartsWith("/swagger");
                        };
                    })
                    .AddHttpClientInstrumentation(options =>
                    {
                        options.RecordException = true;
                    })
                    .AddSource(serviceName);

                if (!string.IsNullOrEmpty(jaegerEndpoint))
                {
                    tracing.AddJaegerExporter(options =>
                    {
                        options.AgentHost = new Uri(jaegerEndpoint).Host;
                        options.AgentPort = new Uri(jaegerEndpoint).Port;
                        options.ExportProcessorType = ExportProcessorType.Simple;
                    });
                }
            });

        return services;
    }
}
