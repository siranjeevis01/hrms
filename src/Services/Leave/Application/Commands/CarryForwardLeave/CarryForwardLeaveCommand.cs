using MediatR;

namespace HRMS.Services.Leave.Application.Commands.CarryForwardLeave;

public class CarryForwardLeaveCommand : IRequest<bool>
{
    public Guid EmployeeId { get; set; }
    public Guid LeaveTypeId { get; set; }
    public int FromYear { get; set; }
    public int ToYear { get; set; }
    public Guid? TenantId { get; set; }
}
