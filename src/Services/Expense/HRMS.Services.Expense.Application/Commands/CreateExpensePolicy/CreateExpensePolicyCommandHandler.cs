using HRMS.Services.Expense.Application.Interfaces;
using HRMS.Services.Expense.Domain.Entities;
using MediatR;

namespace HRMS.Services.Expense.Application.Commands.CreateExpensePolicy;

public class CreateExpensePolicyCommandHandler : IRequestHandler<CreateExpensePolicyCommand, Guid>
{
    private readonly IExpenseDbContext _context;

    public CreateExpensePolicyCommandHandler(IExpenseDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateExpensePolicyCommand request, CancellationToken cancellationToken)
    {
        var policy = ExpensePolicy.Create(
            request.Name,
            request.Description,
            request.Category,
            request.MaxAmount,
            request.Currency,
            request.RequiresReceipt,
            request.ApprovalRequired,
            request.TenantId);

        _context.ExpensePolicies.Add(policy);
        await _context.SaveChangesAsync(cancellationToken);

        return policy.Id;
    }
}
