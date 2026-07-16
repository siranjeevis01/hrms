using System.Collections.Concurrent;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Http;
using Microsoft.ReverseProxy.Middleware;

namespace HRMS.ApiGateway.Transforms;

public sealed class RateLimitTransform : ITransformer
{
    private static readonly ConcurrentDictionary<string, FixedWindowRateLimiter> Limiters = new();

    private static readonly Dictionary<string, FixedWindowRateLimiterOptions> RouteLimits = new(StringComparer.OrdinalIgnoreCase)
    {
        ["/api/identity/"] = new()
        {
            PermitLimit = 10,
            Window = TimeSpan.FromSeconds(60),
            QueueLimit = 0
        },
        ["/api/attendance/"] = new()
        {
            PermitLimit = 60,
            Window = TimeSpan.FromSeconds(60),
            QueueLimit = 5
        },
        ["/api/payroll/"] = new()
        {
            PermitLimit = 20,
            Window = TimeSpan.FromSeconds(60),
            QueueLimit = 2
        },
        ["/api/recruitment/"] = new()
        {
            PermitLimit = 30,
            Window = TimeSpan.FromSeconds(60),
            QueueLimit = 5
        },
        ["/api/notifications/"] = new()
        {
            PermitLimit = 100,
            Window = TimeSpan.FromSeconds(60),
            QueueLimit = 10
        },
    };

    private const int DefaultPermitLimit = 50;
    private static readonly TimeSpan DefaultWindow = TimeSpan.FromSeconds(60);

    public async ValueTask TransformRequestAsync(
        HttpContext httpContext,
        ClusterPartition cluster,
        CancellationToken cancellationToken)
    {
        var path = httpContext.Request.Path.Value ?? string.Empty;
        var limiter = GetOrCreateLimiter(path);

        var lease = await limiter.AcquireAsync(permitCount: 1, cancellationToken);

        if (!lease.IsAcquired)
        {
            httpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            httpContext.Response.Headers["Retry-After"] = lease.GetRetryAfter()?.TotalSeconds.ToString("0") ?? "60";

            if (lease.TryGetMetadata(out var metadata))
            {
                httpContext.Response.Headers["X-RateLimit-Limit"] = metadata.PermitLimit.ToString();
                httpContext.Response.Headers["X-RateLimit-Remaining"] = metadata.GetRetryAfter().ToString();
            }

            lease.Dispose();
            httpContext.SetEndpoint(null!);
            httpContext.SetReverseProxyFeature(null!);
            return;
        }

        lease.Dispose();
        await ValueTask.CompletedTask;
    }

    public ValueTask TransformResponseAsync(
        HttpContext httpContext,
        ClusterPartition cluster,
        CancellationToken cancellationToken)
    {
        var path = httpContext.Request.Path.Value ?? string.Empty;
        var limiter = GetOrCreateLimiter(path);
        var statistics = limiter.GetStatistics();

        httpContext.Response.Headers["X-RateLimit-Limit"] = limiter.GetStatistics().TotalSuccessfulRequestCount.ToString();
        httpContext.Response.Headers["X-RateLimit-Remaining"] = "N/A";

        return ValueTask.CompletedTask;
    }

    public ValueTask TransformResponseTrailersAsync(
        HttpContext httpContext,
        ClusterPartition cluster,
        CancellationToken cancellationToken)
    {
        return ValueTask.CompletedTask;
    }

    private static FixedWindowRateLimiter GetOrCreateLimiter(string path)
    {
        var key = RouteLimits.Keys.FirstOrDefault(k => path.StartsWith(k, StringComparison.OrdinalIgnoreCase))
            ?? "default";

        return Limiters.GetOrAdd(key, _ =>
        {
            if (RouteLimits.TryGetValue(key, out var options))
            {
                return new FixedWindowRateLimiter(options);
            }

            return new FixedWindowRateLimiter(new FixedWindowRateLimiterOptions
            {
                PermitLimit = DefaultPermitLimit,
                Window = DefaultWindow,
                QueueLimit = 5
            });
        });
    }
}
