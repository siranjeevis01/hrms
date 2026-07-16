using HRMS.Services.Expense.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Expense.Application.Commands.RemoveExpenseItem;

public class RemoveExpenseItemCommandHandler : IRequestHandler<RemoveExpenseItemCommand>
{
    private readonly IExpenseDbContext _context;

    public RemoveExpenseItemCommandHandler(IExpenseDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RemoveExpenseItemCommand request, CancellationToken cancellationToken)
    {
        var claim = await _context.ExpenseClaims
            .FirstOrDefaultAsync(c => c.Id == request.ClaimId, cancellationToken);

        if (claim == null)
            throw new InvalidOperationException($"Expense claim with ID {request.ClaimId} not found.");

        var item = await _context.ExpenseItems
            .FirstOrDefaultAsync(i => i.Id == request.ItemId, cancellationToken);

        if (item == null)
            throw new InvalidOperationException($"Expense item with ID {request.ItemId} not found.");

        claim.RemoveItem(request.ItemId);
        _context.ExpenseItems.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
