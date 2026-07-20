using HRMS.Services.Payroll.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Payroll.Domain.Entities;

public class Loan : BaseEntity
{
    public new Guid Id { get; private set; }
    public Guid EmployeeId { get; private set; }
    public LoanType LoanType { get; private set; }
    public decimal Amount { get; private set; }
    public decimal OutstandingAmount { get; private set; }
    public decimal MonthlyDeduction { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }
    public new LoanStatus Status { get; private set; }
    public Guid? ApprovedBy { get; private set; }
    public new Guid TenantId { get; private set; }

    private readonly List<LoanRepayment> _repayments = new();
    public IReadOnlyCollection<LoanRepayment> Repayments => _repayments.AsReadOnly();

    private Loan() { }

    public Loan(Guid employeeId, LoanType loanType, decimal amount, decimal monthlyDeduction,
        DateOnly startDate, Guid tenantId)
    {
        Id = Guid.NewGuid();
        EmployeeId = employeeId;
        LoanType = loanType;
        Amount = amount;
        OutstandingAmount = amount;
        MonthlyDeduction = monthlyDeduction;
        StartDate = startDate;
        Status = LoanStatus.Active;
        TenantId = tenantId;
        CreatedAt = DateTime.UtcNow;
    }

    public void Approve(Guid approvedBy)
    {
        ApprovedBy = approvedBy;
        UpdatedAt = DateTime.UtcNow;
    }

    public void DeductPayment(decimal amount, Guid employeePayrollId)
    {
        if (Status != LoanStatus.Active)
            throw new InvalidOperationException("Only active loans can have payments deducted.");

        if (amount > OutstandingAmount)
            amount = OutstandingAmount;

        OutstandingAmount -= amount;
        var repayment = new LoanRepayment(Id, employeePayrollId, amount, DateTime.UtcNow, OutstandingAmount);
        _repayments.Add(repayment);

        if (OutstandingAmount <= 0)
        {
            Close();
        }

        UpdatedAt = DateTime.UtcNow;
    }

    public void Close()
    {
        Status = LoanStatus.Closed;
        OutstandingAmount = 0;
        EndDate = DateOnly.FromDateTime(DateTime.UtcNow);
        UpdatedAt = DateTime.UtcNow;
    }

    public void Default()
    {
        Status = LoanStatus.Defaulted;
        UpdatedAt = DateTime.UtcNow;
    }
}
