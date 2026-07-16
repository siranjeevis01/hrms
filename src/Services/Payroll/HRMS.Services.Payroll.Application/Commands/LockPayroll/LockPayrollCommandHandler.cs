using HRMS.Services.Payroll.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Commands.LockPayroll;

public class LockPayrollCommandHandler : IRequestHandler<LockPayrollCommand>
{
    private readonly IPayrollDbContext _context;

    public LockPayrollCommandHandler(IPayrollDbContext context)
    {
        _context = context;
    }

    public async Task Handle(LockPayrollCommand request, CancellationToken cancellationToken)
    {
        var payrollRun = await _context.PayrollRuns
            .FirstOrDefaultAsync(r => r.Id == request.PayrollRunId, cancellationToken)
            ?? throw new InvalidOperationException($"Payroll run {request.PayrollRunId} not found.");

        payrollRun.Lock(request.LockedBy);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
