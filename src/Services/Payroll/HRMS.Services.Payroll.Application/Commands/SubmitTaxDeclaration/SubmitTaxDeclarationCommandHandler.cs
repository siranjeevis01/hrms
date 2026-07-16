using HRMS.Services.Payroll.Application.Interfaces;
using HRMS.Services.Payroll.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Commands.SubmitTaxDeclaration;

public class SubmitTaxDeclarationCommandHandler : IRequestHandler<SubmitTaxDeclarationCommand, Guid>
{
    private readonly IPayrollDbContext _context;

    public SubmitTaxDeclarationCommandHandler(IPayrollDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(SubmitTaxDeclarationCommand request, CancellationToken cancellationToken)
    {
        var existing = await _context.EmployeeTaxDeclarations
            .FirstOrDefaultAsync(d => d.EmployeeId == request.EmployeeId
                && d.FinancialYear == request.FinancialYear, cancellationToken);

        if (existing != null)
        {
            _context.EmployeeTaxDeclarations.Remove(existing);
        }

        var declaration = new EmployeeTaxDeclaration(
            request.EmployeeId, request.FinancialYear, request.DeclaredAmount,
            request.InvestedAmount, request.ProofSubmitted, request.TenantId);

        _context.EmployeeTaxDeclarations.Add(declaration);
        await _context.SaveChangesAsync(cancellationToken);

        return declaration.Id;
    }
}
