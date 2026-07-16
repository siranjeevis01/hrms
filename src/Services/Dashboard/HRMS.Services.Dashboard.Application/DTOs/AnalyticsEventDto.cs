using HRMS.Services.Dashboard.Domain.Enums;

namespace HRMS.Services.Dashboard.Application.DTOs;

public class AnalyticsEventDto
{
    public Guid Id { get; set; }
    public AnalyticsEventType EventType { get; set; }
    public string? EntityId { get; set; }
    public string? EntityType { get; set; }
    public Guid? EmployeeId { get; set; }
    public string? Data { get; set; }
    public DateTime Timestamp { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
