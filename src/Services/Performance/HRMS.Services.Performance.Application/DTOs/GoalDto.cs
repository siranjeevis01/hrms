using HRMS.Services.Performance.Domain.Enums;

namespace HRMS.Services.Performance.Application.DTOs;

public class GoalDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public GoalCategory Category { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid? ManagerId { get; set; }
    public Guid? DepartmentId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public GoalStatus Status { get; set; }
    public GoalPriority Priority { get; set; }
    public decimal Weight { get; set; }
    public decimal? TargetValue { get; set; }
    public decimal? CurrentValue { get; set; }
    public string? Unit { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public List<KeyResultDto> KeyResults { get; set; } = new();
}
