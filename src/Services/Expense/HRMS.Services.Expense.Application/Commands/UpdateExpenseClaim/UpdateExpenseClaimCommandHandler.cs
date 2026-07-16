using HRMS.Services.Expense.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Expense.Application.Commands.UpdateExpenseClaim;

public class UpdateExpenseClaimCommandHandler : IRequestHandler<UpdateExpenseClaimCommand>
{
    private readonly IExpenseDbContext _context;

    public UpdateExpenseClaimCommandHandler(IExpenseDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateExpenseClaimCommand request, CancellationToken cancellationToken)
    {
        var claim = await _context.ExpenseClaims
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (claim == null)
            throw new InvalidOperationException($"Expense claim with ID {request.Id} not found.");

        claim.Update(request.Title, request.Description, request.Currency);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
