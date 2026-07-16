using HRMS.Services.Payroll.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Payroll.Domain.Entities;

public class EmployeeTaxDeclaration : BaseEntity
{
    public Guid Id { get; private set; }
    public Guid EmployeeId { get; private set; }
    public string FinancialYear { get; private set; } = string.Empty;
    public decimal DeclaredAmount { get; private set; }
    public decimal InvestedAmount { get; private set; }
    public bool ProofSubmitted { get; private set; }
    public Guid? VerifiedBy { get; private set; }
    public DateTime? VerifiedAt { get; private set; }
    public PayrollStatus Status { get; private set; }
    public Guid TenantId { get; private set; }

    private EmployeeTaxDeclaration() { }

    public EmployeeTaxDeclaration(Guid employeeId, string financialYear, decimal declaredAmount,
        decimal investedAmount, bool proofSubmitted, Guid tenantId)
    {
        Id = Guid.NewGuid();
        EmployeeId = employeeId;
        FinancialYear = financialYear;
        DeclaredAmount = declaredAmount;
        InvestedAmount = investedAmount;
        ProofSubmitted = proofSubmitted;
        Status = PayrollStatus.Draft;
        TenantId = tenantId;
        CreatedAt = DateTime.UtcNow;
    }

    public void Verify(Guid verifiedBy, bool approved)
    {
        VerifiedBy = verifiedBy;
        VerifiedAt = DateTime.UtcNow;
        Status = approved ? PayrollStatus.Approved : PayrollStatus.Reversed;
        UpdatedAt = DateTime.UtcNow;
    }
}
