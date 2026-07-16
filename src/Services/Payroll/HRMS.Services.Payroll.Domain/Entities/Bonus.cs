using HRMS.Services.Payroll.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Payroll.Domain.Entities;

public class Bonus : BaseEntity
{
    public Guid Id { get; private set; }
    public Guid EmployeeId { get; private set; }
    public BonusType BonusType { get; private set; }
    public decimal Amount { get; private set; }
    public int Month { get; private set; }
    public int Year { get; private set; }
    public PayrollStatus Status { get; private set; }
    public Guid? ApprovedBy { get; private set; }
    public DateTime? ApprovedAt { get; private set; }
    public Guid TenantId { get; private set; }

    private Bonus() { }

    public Bonus(Guid employeeId, BonusType bonusType, decimal amount, int month, int year, Guid tenantId)
    {
        Id = Guid.NewGuid();
        EmployeeId = employeeId;
        BonusType = bonusType;
        Amount = amount;
        Month = month;
        Year = year;
        Status = PayrollStatus.Draft;
        TenantId = tenantId;
        CreatedAt = DateTime.UtcNow;
    }

    public void Approve(Guid approvedBy)
    {
        if (Status != PayrollStatus.Draft)
            throw new InvalidOperationException("Only Draft bonuses can be approved.");

        Status = PayrollStatus.Approved;
        ApprovedBy = approvedBy;
        ApprovedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkPaid()
    {
        if (Status != PayrollStatus.Approved)
            throw new InvalidOperationException("Only Approved bonuses can be marked as paid.");

        Status = PayrollStatus.Paid;
        UpdatedAt = DateTime.UtcNow;
    }
}
