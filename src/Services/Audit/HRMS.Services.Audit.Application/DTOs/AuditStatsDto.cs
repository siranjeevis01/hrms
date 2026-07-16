using HRMS.Services.Audit.Domain.Enums;

namespace HRMS.Services.Audit.Application.DTOs;

public class AuditStatsDto
{
    public int TotalAuditLogs { get; set; }
    public int TotalLoginAttempts { get; set; }
    public int SuccessfulLogins { get; set; }
    public int FailedLogins { get; set; }
    public int TotalDataChanges { get; set; }
    public Dictionary<AuditActionType, int> ActionsByType { get; set; } = new();
    public Dictionary<AuditEntityType, int> ChangesByEntity { get; set; } = new();
    public List<AuditActivityDto> RecentActivity { get; set; } = new();
}

public class AuditActivityDto
{
    public string Action { get; set; } = string.Empty;
    public string Entity { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}
