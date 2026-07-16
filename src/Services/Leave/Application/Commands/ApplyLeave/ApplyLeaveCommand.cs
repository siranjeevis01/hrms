using MediatR;

namespace HRMS.Services.Leave.Application.Commands.ApplyLeave;

public class ApplyLeaveCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public Guid LeaveTypeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsHalfDay { get; set; }
    public string? HalfDayType { get; set; }
    public string Reason { get; set; } = string.Empty;
    public Guid? TenantId { get; set; }
}
