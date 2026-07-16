using HRMS.Shared.Kernel.Tenancy;

namespace HRMS.Shared.Kernel.Interfaces;

public interface ITenantService
{
    TenantInfo? GetCurrentTenant();
    void SetCurrentTenant(TenantInfo tenant);
    Task<TenantInfo?> GetTenantByIdAsync(Guid tenantId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TenantInfo>> GetAllTenantsAsync(CancellationToken cancellationToken = default);
}
