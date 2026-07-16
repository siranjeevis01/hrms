using HRMS.Services.Travel.Application.Interfaces;
using HRMS.Services.Travel.Domain.Entities;
using MediatR;

namespace HRMS.Services.Travel.Application.Commands.AddTravelExpense;

public class AddTravelExpenseCommandHandler : IRequestHandler<AddTravelExpenseCommand, Guid>
{
    private readonly ITravelDbContext _context;

    public AddTravelExpenseCommandHandler(ITravelDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddTravelExpenseCommand request, CancellationToken cancellationToken)
    {
        var expense = TravelExpense.Create(
            request.TravelRequestId,
            request.Category,
            request.Description,
            request.Amount,
            request.Currency,
            request.ReceiptUrl,
            request.Date,
            request.TenantId);

        _context.TravelExpenses.Add(expense);
        await _context.SaveChangesAsync(cancellationToken);

        return expense.Id;
    }
}
