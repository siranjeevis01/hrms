using HRMS.Services.Payroll.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Commands.ReversePayroll;

public class ReversePayrollCommandHandler : IRequestHandler<ReversePayrollCommand>
{
    private readonly IPayrollDbContext _context;

    public ReversePayrollCommandHandler(IPayrollDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ReversePayrollCommand request, CancellationToken cancellationToken)
    {
        var payrollRun = await _context.PayrollRuns
            .FirstOrDefaultAsync(r => r.Id == request.PayrollRunId, cancellationToken)
            ?? throw new InvalidOperationException($"Payroll run {request.PayrollRunId} not found.");

        payrollRun.Reverse(request.ReversedBy, request.Reason);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
