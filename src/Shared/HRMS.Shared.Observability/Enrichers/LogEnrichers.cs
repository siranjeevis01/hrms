using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;

namespace HRMS.Shared.Observability.Enrichers;

public class TenantIdEnricher : ILogEventEnricher
{
    private readonly IHttpContextAccessor? _httpContextAccessor;

    public TenantIdEnricher() { }

    public TenantIdEnricher(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var httpContext = _httpContextAccessor?.HttpContext;
        var tenantId = httpContext?.Request?.Headers["X-Tenant-Id"].FirstOrDefault();
        if (!string.IsNullOrEmpty(tenantId))
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("TenantId", tenantId));
        }
    }
}

public class UserIdEnricher : ILogEventEnricher
{
    private readonly IHttpContextAccessor? _httpContextAccessor;

    public UserIdEnricher() { }

    public UserIdEnricher(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var userId = _httpContextAccessor?.HttpContext?.User?.FindFirst("sub")?.Value
                     ?? _httpContextAccessor?.HttpContext?.User?.FindFirst("user_id")?.Value;

        if (!string.IsNullOrEmpty(userId))
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("UserId", userId));
        }
    }
}

public class CorrelationIdEnricher : ILogEventEnricher
{
    private readonly IHttpContextAccessor? _httpContextAccessor;

    public CorrelationIdEnricher() { }

    public CorrelationIdEnricher(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var correlationId = _httpContextAccessor?.HttpContext?.Request?.Headers["X-Correlation-Id"].FirstOrDefault()
                            ?? _httpContextAccessor?.HttpContext?.TraceIdentifier;

        if (!string.IsNullOrEmpty(correlationId))
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("CorrelationId", correlationId));
        }
    }
}
