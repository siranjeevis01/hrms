using MediatR;

namespace HRMS.Services.Payroll.Application.Commands.VerifyTaxDeclaration;

public class VerifyTaxDeclarationCommand : IRequest
{
    public Guid DeclarationId { get; set; }
    public Guid VerifiedBy { get; set; }
    public bool Approved { get; set; }
}
