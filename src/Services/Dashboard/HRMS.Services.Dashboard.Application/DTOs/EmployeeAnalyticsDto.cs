using HRMS.Services.Dashboard.Domain.Enums;

namespace HRMS.Services.Dashboard.Application.DTOs;

public class EmployeeAnalyticsDto
{
    public Guid EmployeeId { get; set; }
    public int TotalEvents { get; set; }
    public int PageViews { get; set; }
    public int Actions { get; set; }
    public int Filters { get; set; }
    public int Exports { get; set; }
    public DateTime? LastActivityAt { get; set; }
    public Dictionary<string, int> ActivityByEntity { get; set; } = new();
    public List<AnalyticsEventDto> RecentEvents { get; set; } = new();
}
