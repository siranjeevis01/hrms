using HRMS.Services.Performance.Domain.Enums;

namespace HRMS.Services.Performance.Application.DTOs;

public class KeyResultDto
{
    public Guid Id { get; set; }
    public Guid GoalId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal TargetValue { get; set; }
    public decimal CurrentValue { get; set; }
    public string? Unit { get; set; }
    public decimal Weight { get; set; }
    public GoalStatus Status { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
