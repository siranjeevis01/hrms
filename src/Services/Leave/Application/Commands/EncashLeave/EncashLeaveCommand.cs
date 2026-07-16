using MediatR;

namespace HRMS.Services.Leave.Application.Commands.EncashLeave;

public class EncashLeaveCommand : IRequest<bool>
{
    public Guid EmployeeId { get; set; }
    public Guid LeaveTypeId { get; set; }
    public decimal Days { get; set; }
    public string? Reason { get; set; }
    public Guid? TenantId { get; set; }
}
