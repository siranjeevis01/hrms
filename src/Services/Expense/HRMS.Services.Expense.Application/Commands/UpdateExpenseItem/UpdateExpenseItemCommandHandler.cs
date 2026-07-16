using HRMS.Services.Expense.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Expense.Application.Commands.UpdateExpenseItem;

public class UpdateExpenseItemCommandHandler : IRequestHandler<UpdateExpenseItemCommand>
{
    private readonly IExpenseDbContext _context;

    public UpdateExpenseItemCommandHandler(IExpenseDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateExpenseItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.ExpenseItems
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (item == null)
            throw new InvalidOperationException($"Expense item with ID {request.Id} not found.");

        item.Update(request.Category, request.Description, request.Amount, request.Currency, request.Date, request.ReceiptUrl, request.IsReimbursable);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
