using HRMS.Services.Dashboard.Application.Interfaces;
using HRMS.Services.Dashboard.Domain.Entities;
using HRMS.Shared.Kernel.Common;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Dashboard.Infrastructure.Persistence;

public class DashboardDbContext : DbContext, IDashboardDbContext
{
    public DashboardDbContext(DbContextOptions<DashboardDbContext> options) : base(options) { }

    public DbSet<HRMS.Services.Dashboard.Domain.Entities.Dashboard> Dashboards => Set<HRMS.Services.Dashboard.Domain.Entities.Dashboard>();
    public DbSet<DashboardWidget> DashboardWidgets => Set<DashboardWidget>();
    public DbSet<WidgetPreset> WidgetPresets => Set<WidgetPreset>();
    public DbSet<DashboardShare> DashboardShares => Set<DashboardShare>();
    public DbSet<AnalyticsEvent> AnalyticsEvents => Set<AnalyticsEvent>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DashboardDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
