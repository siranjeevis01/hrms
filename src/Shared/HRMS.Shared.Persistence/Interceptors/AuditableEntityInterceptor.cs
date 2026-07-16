using HRMS.Shared.Kernel.Common;
using HRMS.Shared.Kernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HRMS.Shared.Persistence.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ITenantService _tenantService;

    public AuditableEntityInterceptor(
        ICurrentUserService currentUserService,
        ITenantService tenantService)
    {
        _currentUserService = currentUserService;
        _tenantService = tenantService;
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData, InterceptionResult<int> result)
    {
        ApplyAuditInfo(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplyAuditInfo(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void ApplyAuditInfo(DbContext? context)
    {
        if (context is null) return;

        var now = DateTime.UtcNow;
        var userId = _currentUserService.UserId?.ToString();

        foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = now;
                    entry.Entity.CreatedBy = userId;
                    if (entry.Entity.TenantId == Guid.Empty)
                    {
                        var tenant = _tenantService.GetCurrentTenant();
                        if (tenant is not null)
                        {
                            entry.Entity.TenantId = tenant.Id;
                        }
                    }
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = now;
                    entry.Entity.UpdatedBy = userId;
                    entry.Property(e => e.CreatedAt).IsModified = false;
                    entry.Property(e => e.CreatedBy).IsModified = false;
                    entry.Property(e => e.TenantId).IsModified = false;
                    break;
            }
        }
    }
}
