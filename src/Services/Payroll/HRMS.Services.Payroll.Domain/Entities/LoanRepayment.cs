using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Payroll.Domain.Entities;

public class LoanRepayment : BaseEntity
{
    public Guid Id { get; private set; }
    public Guid LoanId { get; private set; }
    public Guid EmployeePayrollId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime PaidDate { get; private set; }
    public decimal RemainingBalance { get; private set; }

    public Loan Loan { get; private set; } = null!;
    public EmployeePayroll EmployeePayroll { get; private set; } = null!;

    private LoanRepayment() { }

    public LoanRepayment(Guid loanId, Guid employeePayrollId, decimal amount, DateTime paidDate, decimal remainingBalance)
    {
        Id = Guid.NewGuid();
        LoanId = loanId;
        EmployeePayrollId = employeePayrollId;
        Amount = amount;
        PaidDate = paidDate;
        RemainingBalance = remainingBalance;
        CreatedAt = DateTime.UtcNow;
    }
}
