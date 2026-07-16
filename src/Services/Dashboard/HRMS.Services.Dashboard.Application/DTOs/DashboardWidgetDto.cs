using HRMS.Services.Dashboard.Domain.Enums;

namespace HRMS.Services.Dashboard.Application.DTOs;

public class DashboardWidgetDto
{
    public Guid Id { get; set; }
    public Guid DashboardId { get; set; }
    public WidgetType WidgetType { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? DataSource { get; set; }
    public string? Configuration { get; set; }
    public string? Position { get; set; }
    public string? Size { get; set; }
    public int RefreshIntervalSeconds { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
