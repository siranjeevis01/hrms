using HRMS.Shared.Kernel.Interfaces;
using Microsoft.AspNetCore.Http;

namespace HRMS.Shared.Kernel.Tenancy;

public class TenantResolver
{
    private readonly ITenantService _tenantService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private const string TenantHeaderKey = "X-Tenant-Id";
    private const string TenantClaimType = "tenant_id";

    public TenantResolver(ITenantService tenantService, IHttpContextAccessor httpContextAccessor)
    {
        _tenantService = tenantService;
        _httpContextAccessor = httpContextAccessor;
    }

    public TenantInfo? Resolve()
    {
        var tenantId = ResolveTenantId();
        if (tenantId is null) return null;

        return _tenantService.GetCurrentTenant()?.Id == tenantId
            ? _tenantService.GetCurrentTenant()
            : null;
    }

    public async Task<TenantInfo?> ResolveAsync(CancellationToken cancellationToken = default)
    {
        var tenantId = ResolveTenantId();
        if (tenantId is null) return null;

        var currentTenant = _tenantService.GetCurrentTenant();
        if (currentTenant?.Id == tenantId)
            return currentTenant;

        var tenant = await _tenantService.GetTenantByIdAsync(tenantId.Value, cancellationToken);
        if (tenant is not null)
        {
            _tenantService.SetCurrentTenant(tenant);
        }

        return tenant;
    }

    private Guid? ResolveTenantId()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is null) return null;

        if (httpContext.Request.Headers.TryGetValue(TenantHeaderKey, out var headerValue))
        {
            if (Guid.TryParse(headerValue.ToString(), out var headerTenantId))
                return headerTenantId;
        }

        var user = httpContext.User;
        if (user.Identity?.IsAuthenticated == true)
        {
            var tenantClaim = user.Claims.FirstOrDefault(c => c.Type == TenantClaimType);
            if (tenantClaim is not null && Guid.TryParse(tenantClaim.Value, out var claimTenantId))
                return claimTenantId;
        }

        if (httpContext.Request.Query.TryGetValue(TenantHeaderKey, out var queryValue))
        {
            if (Guid.TryParse(queryValue.ToString(), out var queryTenantId))
                return queryTenantId;
        }

        return null;
    }
}
