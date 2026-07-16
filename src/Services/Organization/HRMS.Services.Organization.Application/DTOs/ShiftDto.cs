namespace HRMS.Services.Organization.Application.DTOs;

public class ShiftDto
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public TimeSpan BreakDuration { get; set; }
    public int GraceMinutes { get; set; }
    public bool IsFlexible { get; set; }
    public int MaxShifts { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
