using MediatR;

namespace HRMS.Services.Leave.Application.Commands.AllocateLeaveBalance;

public class AllocateLeaveBalanceCommand : IRequest<bool>
{
    public List<LeaveBalanceAllocation> Allocations { get; set; } = new();
    public Guid? TenantId { get; set; }
}

public class LeaveBalanceAllocation
{
    public Guid EmployeeId { get; set; }
    public Guid LeaveTypeId { get; set; }
    public int Year { get; set; }
    public decimal TotalDays { get; set; }
}
