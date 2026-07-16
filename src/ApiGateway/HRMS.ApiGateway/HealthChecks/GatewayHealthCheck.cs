using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace HRMS.ApiGateway.HealthChecks;

public sealed class GatewayHealthCheckOptions
{
    public Dictionary<string, string> ServiceEndpoints { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    public int TimeoutSeconds { get; set; } = 5;
    public string[] RequiredServices { get; set; } = [];
}

public sealed class GatewayHealthCheck : IHealthCheck
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<GatewayHealthCheck> _logger;
    private readonly GatewayHealthCheckOptions _options;

    public GatewayHealthCheck(
        IHttpClientFactory httpClientFactory,
        ILogger<GatewayHealthCheck> logger,
        IOptions<GatewayHealthCheckOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _options = options.Value;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var entries = new Dictionary<string, HealthReportEntry>();
        var healthyCount = 0;
        var totalCount = 0;

        var client = _httpClientFactory.CreateClient("HealthCheck");
        client.Timeout = TimeSpan.FromSeconds(_options.TimeoutSeconds);

        foreach (var (serviceName, endpoint) in _options.ServiceEndpoints)
        {
            totalCount++;

            var healthUrl = $"{endpoint}/health";
            try
            {
                var response = await client.GetAsync(healthUrl, cancellationToken);
                var isHealthy = response.IsSuccessStatusCode;

                entries[serviceName] = new HealthReportEntry(
                    isHealthy ? HealthStatus.Healthy : HealthStatus.Degraded,
                    $"HTTP {(int)response.StatusCode}",
                    TimeSpan.Zero,
                    null,
                    null);

                if (isHealthy)
                {
                    healthyCount++;
                }
                else
                {
                    _logger.LogWarning(
                        "Service {ServiceName} at {Url} returned {StatusCode}",
                        serviceName, healthUrl, response.StatusCode);
                }
            }
            catch (TaskCanceledException)
            {
                entries[serviceName] = new HealthReportEntry(
                    HealthStatus.Unhealthy,
                    $"Timeout after {_options.TimeoutSeconds}s",
                    TimeSpan.Zero,
                    null,
                    null);

                _logger.LogWarning("Service {ServiceName} at {Url} timed out", serviceName, healthUrl);
            }
            catch (HttpRequestException ex)
            {
                var status = ex.InnerException is SocketException
                    ? "Unreachable"
                    : ex.Message;

                entries[serviceName] = new HealthReportEntry(
                    HealthStatus.Unhealthy,
                    status,
                    TimeSpan.Zero,
                    ex,
                    null);

                _logger.LogWarning(ex, "Service {ServiceName} at {Url} is unhealthy", serviceName, healthUrl);
            }
            catch (Exception ex)
            {
                entries[serviceName] = new HealthReportEntry(
                    HealthStatus.Unhealthy,
                    ex.Message,
                    TimeSpan.Zero,
                    ex,
                    null);

                _logger.LogError(ex, "Health check failed for {ServiceName}", serviceName);
            }
        }

        var requiredUnhealthy = _options.RequiredServices
            .Where(s => entries.TryGetValue(s, out var e) && e.Status != HealthStatus.Healthy)
            .ToList();

        var overallStatus = requiredUnhealthy.Count > 0
            ? HealthStatus.Unhealthy
            : healthyCount == totalCount
                ? HealthStatus.Healthy
                : HealthStatus.Degraded;

        var report = new HealthReport(entries, TimeSpan.Zero);

        var summary = $"Gateway health: {overallStatus} ({healthyCount}/{totalCount} services healthy)";

        _logger.LogInformation(summary);

        return new HealthCheckResult(
            overallStatus,
            summary,
            data: report.Entries.ToDictionary(
                kvp => kvp.Key,
                kvp => (object)new
                {
                    Status = kvp.Value.Status.ToString(),
                    Description = kvp.Value.Description
                }));
    }
}
