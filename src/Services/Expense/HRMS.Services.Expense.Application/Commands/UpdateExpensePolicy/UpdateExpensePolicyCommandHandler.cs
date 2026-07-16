using HRMS.Services.Expense.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Expense.Application.Commands.UpdateExpensePolicy;

public class UpdateExpensePolicyCommandHandler : IRequestHandler<UpdateExpensePolicyCommand>
{
    private readonly IExpenseDbContext _context;

    public UpdateExpensePolicyCommandHandler(IExpenseDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateExpensePolicyCommand request, CancellationToken cancellationToken)
    {
        var policy = await _context.ExpensePolicies
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (policy == null)
            throw new InvalidOperationException($"Expense policy with ID {request.Id} not found.");

        policy.Update(request.Name, request.Description, request.Category, request.MaxAmount, request.Currency, request.RequiresReceipt, request.ApprovalRequired, request.IsActive);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
