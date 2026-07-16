using MediatR;

namespace HRMS.Services.Leave.Application.Commands.AdjustLeaveBalance;

public class AdjustLeaveBalanceCommand : IRequest<bool>
{
    public Guid EmployeeId { get; set; }
    public Guid LeaveTypeId { get; set; }
    public int Year { get; set; }
    public decimal Days { get; set; }
    public bool IsAddition { get; set; }
    public string? Reason { get; set; }
    public Guid? TenantId { get; set; }
}
