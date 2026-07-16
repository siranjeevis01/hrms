using MediatR;

namespace HRMS.Services.Leave.Application.Commands.CancelLeave;

public class CancelLeaveCommand : IRequest<bool>
{
    public Guid LeaveApplicationId { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid? TenantId { get; set; }
}
