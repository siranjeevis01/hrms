using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HRMS.Shared.Kernel.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Request.Headers["X-Correlation-Id"].FirstOrDefault()
            ?? context.TraceIdentifier;

        var stopwatch = Stopwatch.StartNew();

        _logger.LogInformation(
            "[{CorrelationId}] Incoming request: {Method} {Path}{QueryString}",
            correlationId,
            context.Request.Method,
            context.Request.Path,
            context.Request.QueryString);

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();

            var statusCode = context.Response.StatusCode;
            var level = statusCode >= 400 ? LogLevel.Warning : LogLevel.Information;

            _logger.Log(level,
                "[{CorrelationId}] Request completed: {Method} {Path} responded {StatusCode} in {ElapsedMs}ms",
                correlationId,
                context.Request.Method,
                context.Request.Path,
                statusCode,
                stopwatch.ElapsedMilliseconds);
        }
    }
}
