using HRMS.Services.Payroll.Domain.Enums;

namespace HRMS.Services.Payroll.Application.DTOs;

public class EmployeeTaxDeclarationDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string FinancialYear { get; set; } = string.Empty;
    public decimal DeclaredAmount { get; set; }
    public decimal InvestedAmount { get; set; }
    public bool ProofSubmitted { get; set; }
    public Guid? VerifiedBy { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public PayrollStatus Status { get; set; }
    public Guid TenantId { get; set; }
}
