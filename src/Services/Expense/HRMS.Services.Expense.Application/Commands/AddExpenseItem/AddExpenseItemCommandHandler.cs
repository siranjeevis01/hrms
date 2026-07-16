using HRMS.Services.Expense.Application.Interfaces;
using HRMS.Services.Expense.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Expense.Application.Commands.AddExpenseItem;

public class AddExpenseItemCommandHandler : IRequestHandler<AddExpenseItemCommand, Guid>
{
    private readonly IExpenseDbContext _context;

    public AddExpenseItemCommandHandler(IExpenseDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddExpenseItemCommand request, CancellationToken cancellationToken)
    {
        var claim = await _context.ExpenseClaims
            .FirstOrDefaultAsync(c => c.Id == request.ClaimId, cancellationToken);

        if (claim == null)
            throw new InvalidOperationException($"Expense claim with ID {request.ClaimId} not found.");

        var item = ExpenseItem.Create(
            request.ClaimId,
            request.Category,
            request.Description,
            request.Amount,
            request.Currency,
            request.Date,
            request.ReceiptUrl,
            request.IsReimbursable,
            request.TenantId);

        claim.AddItem(item);
        _context.ExpenseItems.Add(item);
        await _context.SaveChangesAsync(cancellationToken);

        return item.Id;
    }
}
