using HRMS.Services.Payroll.Application.Events;
using HRMS.Services.Payroll.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Commands.ApprovePayroll;

public class ApprovePayrollCommandHandler : IRequestHandler<ApprovePayrollCommand>
{
    private readonly IPayrollDbContext _context;
    private readonly IPublisher _publisher;

    public ApprovePayrollCommandHandler(IPayrollDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(ApprovePayrollCommand request, CancellationToken cancellationToken)
    {
        var payrollRun = await _context.PayrollRuns
            .FirstOrDefaultAsync(r => r.Id == request.PayrollRunId, cancellationToken)
            ?? throw new InvalidOperationException($"Payroll run {request.PayrollRunId} not found.");

        payrollRun.Approve(request.ApprovedBy);
        await _context.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(new PayrollApprovedEvent(
            payrollRun.Id, payrollRun.CompanyId, payrollRun.Month, payrollRun.Year, request.ApprovedBy), cancellationToken);
    }
}
