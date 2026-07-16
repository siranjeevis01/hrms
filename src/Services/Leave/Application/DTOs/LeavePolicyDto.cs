using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Leave.Application.DTOs;

public class LeavePolicyDto : BaseDto
{
    public Guid CompanyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool SandwichPolicyEnabled { get; set; }
    public int? SandwichPolicyDays { get; set; }
    public int MinNoticeDays { get; set; }
    public int MaxPendingApplications { get; set; }
    public bool AllowBackDatedLeave { get; set; }
    public int? BackDatedLimitDays { get; set; }
}
