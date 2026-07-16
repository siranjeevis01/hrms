using HRMS.Shared.Kernel.Interfaces;
using HRMS.Shared.Kernel.Tenancy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HRMS.Shared.Kernel.Middleware;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TenantMiddleware> _logger;

    private const string TenantHeaderKey = "X-Tenant-Id";

    private static readonly HashSet<string> TenantExcludedPaths = new(StringComparer.OrdinalIgnoreCase)
    {
        "/api/tenants",
        "/health",
        "/health/live",
        "/health/ready",
        "/swagger"
    };

    public TenantMiddleware(RequestDelegate next, ILogger<TenantMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (ShouldSkipTenantResolution(context))
        {
            await _next(context);
            return;
        }

        var tenantResolver = context.RequestServices.GetService(typeof(TenantResolver)) as TenantResolver;
        if (tenantResolver is null)
        {
            _logger.LogWarning("TenantResolver not registered. Skipping tenant resolution.");
            await _next(context);
            return;
        }

        var tenant = await tenantResolver.ResolveAsync(context.RequestAborted);

        if (tenant is null)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new
            {
                StatusCode = 400,
                Message = "Tenant identifier is required. Provide a valid X-Tenant-Id header or query parameter."
            };

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(response);
            return;
        }

        if (!tenant.IsActive)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;

            var response = new
            {
                StatusCode = 403,
                Message = $"Tenant '{tenant.Name}' is inactive or suspended."
            };

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(response);
            return;
        }

        if (tenant.ExpiresAt.HasValue && tenant.ExpiresAt.Value < DateTime.UtcNow)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;

            var response = new
            {
                StatusCode = 403,
                Message = $"Tenant '{tenant.Name}' subscription has expired."
            };

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(response);
            return;
        }

        context.Items["TenantInfo"] = tenant;

        _logger.LogDebug("Tenant resolved: {TenantId} ({TenantName})", tenant.Id, tenant.Name);

        await _next(context);
    }

    private static bool ShouldSkipTenantResolution(HttpContext context)
    {
        var path = context.Request.Path.Value ?? string.Empty;
        return TenantExcludedPaths.Any(ep => path.StartsWith(ep, StringComparison.OrdinalIgnoreCase));
    }
}
