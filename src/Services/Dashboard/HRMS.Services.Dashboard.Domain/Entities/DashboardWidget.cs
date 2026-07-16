using HRMS.Services.Dashboard.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Dashboard.Domain.Entities;

public class DashboardWidget : BaseEntity
{
    public Guid DashboardId { get; private set; }
    public WidgetType WidgetType { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? DataSource { get; private set; }
    public string? Configuration { get; private set; }
    public string? Position { get; private set; }
    public string? Size { get; private set; }
    public int RefreshIntervalSeconds { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private DashboardWidget() { }

    public static DashboardWidget Create(
        Guid dashboardId,
        WidgetType widgetType,
        string title,
        string? dataSource,
        string? configuration,
        string? position,
        string? size,
        int refreshIntervalSeconds,
        string tenantId)
    {
        return new DashboardWidget
        {
            Id = Guid.NewGuid(),
            DashboardId = dashboardId,
            WidgetType = widgetType,
            Title = title,
            DataSource = dataSource,
            Configuration = configuration,
            Position = position,
            Size = size,
            RefreshIntervalSeconds = refreshIntervalSeconds,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        WidgetType? widgetType,
        string? title,
        string? dataSource,
        string? configuration,
        string? position,
        string? size,
        int? refreshIntervalSeconds)
    {
        if (widgetType.HasValue) WidgetType = widgetType.Value;
        Title = title ?? Title;
        DataSource = dataSource ?? DataSource;
        Configuration = configuration ?? Configuration;
        Position = position ?? Position;
        Size = size ?? Size;
        if (refreshIntervalSeconds.HasValue) RefreshIntervalSeconds = refreshIntervalSeconds.Value;
        UpdatedAt = DateTime.UtcNow;
    }
}
