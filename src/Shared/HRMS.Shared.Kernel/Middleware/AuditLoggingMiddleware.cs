using System.Security.Claims;
using System.Text.Json;
using HRMS.Shared.Kernel.Enums;
using HRMS.Shared.Kernel.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HRMS.Shared.Kernel.Middleware;

public class AuditLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuditLoggingMiddleware> _logger;

    private static readonly HashSet<string> ExcludedPaths = new(StringComparer.OrdinalIgnoreCase)
    {
        "/health",
        "/health/live",
        "/health/ready",
        "/swagger",
        "/metrics"
    };

    private static readonly HashSet<string> ExcludedMethods = new(StringComparer.OrdinalIgnoreCase)
    {
        "OPTIONS",
        "HEAD"
    };

    public AuditLoggingMiddleware(RequestDelegate next, ILogger<AuditLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (ShouldSkipAudit(context))
        {
            await _next(context);
            return;
        }

        var userId = GetUserId(context);
        var method = context.Request.Method;
        var path = context.Request.Path;
        var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var userAgent = context.Request.Headers["User-Agent"].FirstOrDefault() ?? "unknown";
        var correlationId = context.Request.Headers["X-Correlation-Id"].FirstOrDefault()
            ?? context.TraceIdentifier;

        var auditAction = MapToAuditAction(method);

        _logger.LogInformation(
            "[{CorrelationId}] Audit: {Action} by User={UserId} on {Path} from IP={IpAddress}",
            correlationId,
            auditAction,
            userId ?? "anonymous",
            path,
            ipAddress);

        var auditService = context.RequestServices.GetService(typeof(IAuditService)) as IAuditService;
        if (auditService is not null && userId is not null && Guid.TryParse(userId, out var parsedUserId))
        {
            var details = JsonSerializer.Serialize(new
            {
                Method = method,
                Path = path.Value,
                QueryString = context.Request.QueryString.Value,
                IpAddress = ipAddress,
                UserAgent = userAgent,
                CorrelationId = correlationId
            });

            await auditService.LogAsync(
                auditAction,
                "HttpContext",
                parsedUserId,
                performedBy: userId,
                details: details,
                cancellationToken: context.RequestAborted);
        }

        await _next(context);
    }

    private static bool ShouldSkipAudit(HttpContext context)
    {
        var path = context.Request.Path.Value ?? string.Empty;
        return ExcludedPaths.Any(ep => path.StartsWith(ep, StringComparison.OrdinalIgnoreCase)) ||
               ExcludedMethods.Contains(context.Request.Method);
    }

    private static string? GetUserId(HttpContext context)
    {
        return context.User?.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? context.User?.FindFirstValue("sub")
            ?? context.User?.FindFirstValue("user_id");
    }

    private static AuditAction MapToAuditAction(string method)
    {
        return method.ToUpperInvariant() switch
        {
            "GET" => AuditAction.View,
            "POST" => AuditAction.Create,
            "PUT" or "PATCH" => AuditAction.Update,
            "DELETE" => AuditAction.Delete,
            _ => AuditAction.View
        };
    }
}
