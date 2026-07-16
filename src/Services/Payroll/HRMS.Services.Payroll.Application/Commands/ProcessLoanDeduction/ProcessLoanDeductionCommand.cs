using MediatR;

namespace HRMS.Services.Payroll.Application.Commands.ProcessLoanDeduction;

public class ProcessLoanDeductionCommand : IRequest<List<LoanDeductionResult>>
{
    public Guid PayrollRunId { get; set; }
    public Guid TenantId { get; set; }
}

public class LoanDeductionResult
{
    public Guid LoanId { get; set; }
    public Guid EmployeeId { get; set; }
    public decimal DeductedAmount { get; set; }
    public decimal RemainingBalance { get; set; }
}
