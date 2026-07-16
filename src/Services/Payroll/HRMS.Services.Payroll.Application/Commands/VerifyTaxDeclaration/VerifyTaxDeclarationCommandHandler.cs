using HRMS.Services.Payroll.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Commands.VerifyTaxDeclaration;

public class VerifyTaxDeclarationCommandHandler : IRequestHandler<VerifyTaxDeclarationCommand>
{
    private readonly IPayrollDbContext _context;

    public VerifyTaxDeclarationCommandHandler(IPayrollDbContext context)
    {
        _context = context;
    }

    public async Task Handle(VerifyTaxDeclarationCommand request, CancellationToken cancellationToken)
    {
        var declaration = await _context.EmployeeTaxDeclarations
            .FirstOrDefaultAsync(d => d.Id == request.DeclarationId, cancellationToken)
            ?? throw new InvalidOperationException($"Tax declaration {request.DeclarationId} not found.");

        declaration.Verify(request.VerifiedBy, request.Approved);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
