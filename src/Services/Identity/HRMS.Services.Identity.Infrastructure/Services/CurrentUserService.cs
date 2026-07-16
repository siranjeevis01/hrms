using System.Security.Claims;
using HRMS.Shared.Kernel.Enums;
using HRMS.Shared.Kernel.Interfaces;
using Microsoft.AspNetCore.Http;

namespace HRMS.Services.Identity.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId
    {
        get
        {
            var sub = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                      ?? _httpContextAccessor.HttpContext?.User.FindFirst("sub")?.Value;
            return sub is not null && Guid.TryParse(sub, out var userId) ? userId : null;
        }
    }

    public Guid? TenantId
    {
        get
        {
            var tenantClaim = _httpContextAccessor.HttpContext?.User.FindFirst("tenant_id")?.Value;
            return tenantClaim is not null && Guid.TryParse(tenantClaim, out var tenantId) ? tenantId : null;
        }
    }

    public string? Email =>
        _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value
        ?? _httpContextAccessor.HttpContext?.User.FindFirst("email")?.Value;

    public string? FullName =>
        _httpContextAccessor.HttpContext?.User.FindFirst("name")?.Value
        ?? _httpContextAccessor.HttpContext?.User.FindFirst("full_name")?.Value;

    public IReadOnlyList<UserRole> Roles
    {
        get
        {
            var roleClaims = _httpContextAccessor.HttpContext?.User.FindAll(ClaimTypes.Role) ?? Enumerable.Empty<Claim>();
            var roles = new List<UserRole>();

            foreach (var claim in roleClaims)
            {
                if (Enum.TryParse<UserRole>(claim.Value, out var role))
                    roles.Add(role);
            }

            return roles.AsReadOnly();
        }
    }

    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

    public bool IsInRole(UserRole role)
    {
        return Roles.Contains(role);
    }
}
