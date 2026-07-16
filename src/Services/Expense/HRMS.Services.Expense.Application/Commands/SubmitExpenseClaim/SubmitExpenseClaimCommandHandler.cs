using HRMS.Services.Expense.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Expense.Application.Commands.SubmitExpenseClaim;

public class SubmitExpenseClaimCommandHandler : IRequestHandler<SubmitExpenseClaimCommand>
{
    private readonly IExpenseDbContext _context;

    public SubmitExpenseClaimCommandHandler(IExpenseDbContext context)
    {
        _context = context;
    }

    public async Task Handle(SubmitExpenseClaimCommand request, CancellationToken cancellationToken)
    {
        var claim = await _context.ExpenseClaims
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (claim == null)
            throw new InvalidOperationException($"Expense claim with ID {request.Id} not found.");

        claim.Submit();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
