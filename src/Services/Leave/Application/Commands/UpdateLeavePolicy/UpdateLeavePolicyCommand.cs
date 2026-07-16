using MediatR;

namespace HRMS.Services.Leave.Application.Commands.UpdateLeavePolicy;

public class UpdateLeavePolicyCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool SandwichPolicyEnabled { get; set; }
    public int? SandwichPolicyDays { get; set; }
    public int MinNoticeDays { get; set; }
    public int MaxPendingApplications { get; set; }
    public bool AllowBackDatedLeave { get; set; }
    public int? BackDatedLimitDays { get; set; }
    public Guid? TenantId { get; set; }
}
