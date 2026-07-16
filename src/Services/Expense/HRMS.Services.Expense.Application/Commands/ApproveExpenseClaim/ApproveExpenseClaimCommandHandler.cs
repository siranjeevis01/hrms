using HRMS.Services.Expense.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Expense.Application.Commands.ApproveExpenseClaim;

public class ApproveExpenseClaimCommandHandler : IRequestHandler<ApproveExpenseClaimCommand>
{
    private readonly IExpenseDbContext _context;

    public ApproveExpenseClaimCommandHandler(IExpenseDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ApproveExpenseClaimCommand request, CancellationToken cancellationToken)
    {
        var claim = await _context.ExpenseClaims
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (claim == null)
            throw new InvalidOperationException($"Expense claim with ID {request.Id} not found.");

        claim.Approve(request.ReviewedBy, request.Comments);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
