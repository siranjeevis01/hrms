using MediatR;

namespace HRMS.Services.Payroll.Application.Events;

public class LoanApprovedEvent : INotification
{
    public Guid LoanId { get; }
    public Guid EmployeeId { get; }
    public decimal Amount { get; }
    public LoanTypeDto LoanType { get; }

    public LoanApprovedEvent(Guid loanId, Guid employeeId, decimal amount, LoanTypeDto loanType)
    {
        LoanId = loanId;
        EmployeeId = employeeId;
        Amount = amount;
        LoanType = loanType;
    }
}

public enum LoanTypeDto
{
    SalaryAdvance,
    Personal,
    Emergency,
    Education
}
