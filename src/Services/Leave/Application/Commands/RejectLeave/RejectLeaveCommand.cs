using MediatR;

namespace HRMS.Services.Leave.Application.Commands.RejectLeave;

public class RejectLeaveCommand : IRequest<bool>
{
    public Guid LeaveApplicationId { get; set; }
    public Guid RejectorId { get; set; }
    public string? Reason { get; set; }
    public Guid? TenantId { get; set; }
}
