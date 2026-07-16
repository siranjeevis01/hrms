using HRMS.Services.Leave.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Leave.Application.DTOs;

public class LeaveTypeDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Color { get; set; }
    public string? Icon { get; set; }
    public bool IsPaid { get; set; }
    public bool IsUnlimited { get; set; }
    public int DefaultBalanceDays { get; set; }
    public int MaxBalanceDays { get; set; }
    public int MaxCarryForwardDays { get; set; }
    public int MaxEncashmentDays { get; set; }
    public int? CarryForwardExpiryMonths { get; set; }
    public bool AllowEncashment { get; set; }
    public bool AllowCarryForward { get; set; }
    public bool AllowHalfDay { get; set; }
    public int MinDaysPerRequest { get; set; }
    public int MaxDaysPerRequest { get; set; }
    public int? MaxConsecutiveDays { get; set; }
    public bool RequireDocumentation { get; set; }
    public int? DocumentationDaysThreshold { get; set; }
    public string Gender { get; set; } = string.Empty;
    public int? ApplicableAfterDays { get; set; }
    public string AccrualType { get; set; } = string.Empty;
    public decimal AccrualRate { get; set; }
    public bool IsActive { get; set; }
}
