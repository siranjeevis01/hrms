using MediatR;

namespace HRMS.Services.Leave.Application.Commands.ApproveLeave;

public class ApproveLeaveCommand : IRequest<bool>
{
    public Guid LeaveApplicationId { get; set; }
    public Guid ApproverId { get; set; }
    public int? CurrentLevel { get; set; }
    public Guid? TenantId { get; set; }
}
