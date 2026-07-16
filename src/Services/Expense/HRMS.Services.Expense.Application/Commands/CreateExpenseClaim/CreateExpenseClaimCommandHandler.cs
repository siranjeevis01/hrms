using HRMS.Services.Expense.Application.Interfaces;
using HRMS.Services.Expense.Domain.Entities;
using MediatR;

namespace HRMS.Services.Expense.Application.Commands.CreateExpenseClaim;

public class CreateExpenseClaimCommandHandler : IRequestHandler<CreateExpenseClaimCommand, Guid>
{
    private readonly IExpenseDbContext _context;

    public CreateExpenseClaimCommandHandler(IExpenseDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateExpenseClaimCommand request, CancellationToken cancellationToken)
    {
        var claim = ExpenseClaim.Create(
            request.EmployeeId,
            request.Title,
            request.Description,
            request.Currency,
            request.TenantId);

        _context.ExpenseClaims.Add(claim);
        await _context.SaveChangesAsync(cancellationToken);

        return claim.Id;
    }
}
