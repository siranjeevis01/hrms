using HRMS.Services.Expense.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Expense.Application.Commands.RejectExpenseClaim;

public class RejectExpenseClaimCommandHandler : IRequestHandler<RejectExpenseClaimCommand>
{
    private readonly IExpenseDbContext _context;

    public RejectExpenseClaimCommandHandler(IExpenseDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RejectExpenseClaimCommand request, CancellationToken cancellationToken)
    {
        var claim = await _context.ExpenseClaims
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (claim == null)
            throw new InvalidOperationException($"Expense claim with ID {request.Id} not found.");

        claim.Reject(request.ReviewedBy, request.Reason, request.PolicyViolationNotes);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
