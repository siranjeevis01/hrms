using MediatR;

namespace HRMS.Services.Leave.Application.Commands.AccrueLeaveBalance;

public class AccrueLeaveBalanceCommand : IRequest<bool>
{
    public Guid? EmployeeId { get; set; }
    public Guid? LeaveTypeId { get; set; }
    public Guid? TenantId { get; set; }
}
