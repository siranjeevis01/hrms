using MediatR;

namespace HRMS.Services.Payroll.Application.Commands.LockPayroll;

public class LockPayrollCommand : IRequest
{
    public Guid PayrollRunId { get; set; }
    public Guid LockedBy { get; set; }
}
