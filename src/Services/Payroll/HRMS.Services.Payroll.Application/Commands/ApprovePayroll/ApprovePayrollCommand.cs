using MediatR;

namespace HRMS.Services.Payroll.Application.Commands.ApprovePayroll;

public class ApprovePayrollCommand : IRequest
{
    public Guid PayrollRunId { get; set; }
    public Guid ApprovedBy { get; set; }
}
