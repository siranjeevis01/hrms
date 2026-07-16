using HRMS.Shared.Kernel.Common;
using HRMS.Shared.Kernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HRMS.Shared.Persistence.Interceptors;

public class TenantInterceptor : SaveChangesInterceptor
{
    private readonly ITenantService _tenantService;

    public TenantInterceptor(ITenantService tenantService)
    {
        _tenantService = tenantService;
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData, InterceptionResult<int> result)
    {
        ApplyTenantFilter(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplyTenantFilter(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void ApplyTenantFilter(DbContext? context)
    {
        if (context is null) return;

        var tenant = _tenantService.GetCurrentTenant();
        if (tenant is null) return;

        foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added && entry.Entity.TenantId == Guid.Empty)
            {
                entry.Entity.TenantId = tenant.Id;
            }
        }
    }
}
