using System.Diagnostics;

namespace HRMS.ApiGateway.Middleware;

public sealed class RequestTimingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestTimingMiddleware> _logger;

    public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var requestId = context.TraceIdentifier;

        context.Response.Headers["X-Request-Id"] = requestId;
        context.Response.Headers["X-Gateway-Timestamp"] = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();

            var elapsed = stopwatch.ElapsedMilliseconds;
            var method = context.Request.Method;
            var path = context.Request.Path.Value ?? string.Empty;
            var statusCode = context.Response.StatusCode;
            var upstream = context.GetEndpoint()?.Metadata.GetMetadata<Microsoft.ReverseProxy.Runtime.Model.EndpointInfo>();

            using var scope = _logger.BeginScope(new Dictionary<string, object?>
            {
                ["RequestId"] = requestId,
                ["Method"] = method,
                ["Path"] = path,
                ["StatusCode"] = statusCode,
                ["ElapsedMs"] = elapsed,
                ["ClientIp"] = context.Connection.RemoteIpAddress?.ToString() ?? "unknown"
            });

            if (statusCode >= 500)
            {
                _logger.LogError(
                    "HTTP {Method} {Path} completed with {StatusCode} in {ElapsedMs}ms [RequestID: {RequestId}]",
                    method, path, statusCode, elapsed, requestId);
            }
            else if (statusCode >= 400)
            {
                _logger.LogWarning(
                    "HTTP {Method} {Path} completed with {StatusCode} in {ElapsedMs}ms [RequestID: {RequestId}]",
                    method, path, statusCode, elapsed, requestId);
            }
            else
            {
                _logger.LogInformation(
                    "HTTP {Method} {Path} completed with {StatusCode} in {ElapsedMs}ms [RequestID: {RequestId}]",
                    method, path, statusCode, elapsed, requestId);
            }

            context.Response.Headers["X-Response-Time-Ms"] = elapsed.ToString();
        }
    }
}
