using HRMS.Services.Payroll.Application.Interfaces;
using HRMS.Services.Payroll.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Commands.GeneratePayslip;

public class GeneratePayslipCommandHandler : IRequestHandler<GeneratePayslipCommand, Guid>
{
    private readonly IPayrollDbContext _context;

    public GeneratePayslipCommandHandler(IPayrollDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(GeneratePayslipCommand request, CancellationToken cancellationToken)
    {
        var existing = await _context.Payslips
            .FirstOrDefaultAsync(p => p.EmployeePayrollId == request.EmployeePayrollId, cancellationToken);

        if (existing != null)
            return existing.Id;

        var payslip = new Payslip(
            request.EmployeePayrollId, request.EmployeeId, request.Month,
            request.Year, request.PdfUrl, request.TenantId);

        _context.Payslips.Add(payslip);
        await _context.SaveChangesAsync(cancellationToken);

        return payslip.Id;
    }
}
