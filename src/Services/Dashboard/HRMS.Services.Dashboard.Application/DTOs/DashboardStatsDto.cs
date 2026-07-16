using HRMS.Services.Dashboard.Domain.Enums;

namespace HRMS.Services.Dashboard.Application.DTOs;

public class DashboardStatsDto
{
    public int TotalDashboards { get; set; }
    public int TotalWidgets { get; set; }
    public int TotalShares { get; set; }
    public int TotalAnalyticsEvents { get; set; }
    public Dictionary<WidgetType, int> WidgetsByType { get; set; } = new();
    public Dictionary<DashboardCategory, int> DashboardsByCategory { get; set; } = new();
    public List<AnalyticsEventDto> RecentEvents { get; set; } = new();
}
