using HRMS.Services.Dashboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Dashboard.Application.Interfaces;

public interface IDashboardDbContext
{
    DbSet<HRMS.Services.Dashboard.Domain.Entities.Dashboard> Dashboards { get; }
    DbSet<DashboardWidget> DashboardWidgets { get; }
    DbSet<WidgetPreset> WidgetPresets { get; }
    DbSet<DashboardShare> DashboardShares { get; }
    DbSet<AnalyticsEvent> AnalyticsEvents { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
