using MediatR;

namespace HRMS.Services.Payroll.Application.Commands.ReversePayroll;

public class ReversePayrollCommand : IRequest
{
    public Guid PayrollRunId { get; set; }
    public Guid ReversedBy { get; set; }
    public string Reason { get; set; } = string.Empty;
}
