using HRMS.Services.Payroll.Application.Events;
using HRMS.Services.Payroll.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Commands.ApproveLoan;

public class ApproveLoanCommandHandler : IRequestHandler<ApproveLoanCommand>
{
    private readonly IPayrollDbContext _context;
    private readonly IPublisher _publisher;

    public ApproveLoanCommandHandler(IPayrollDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(ApproveLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _context.Loans
            .FirstOrDefaultAsync(l => l.Id == request.LoanId, cancellationToken)
            ?? throw new InvalidOperationException($"Loan {request.LoanId} not found.");

        loan.Approve(request.ApprovedBy);
        await _context.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(new LoanApprovedEvent(
            loan.Id, loan.EmployeeId, loan.Amount,
            (LoanTypeDto)(int)loan.LoanType), cancellationToken);
    }
}
