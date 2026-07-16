using MediatR;

namespace HRMS.Services.Payroll.Application.Commands.SubmitTaxDeclaration;

public class SubmitTaxDeclarationCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public string FinancialYear { get; set; } = string.Empty;
    public decimal DeclaredAmount { get; set; }
    public decimal InvestedAmount { get; set; }
    public bool ProofSubmitted { get; set; }
    public Guid TenantId { get; set; }
}
