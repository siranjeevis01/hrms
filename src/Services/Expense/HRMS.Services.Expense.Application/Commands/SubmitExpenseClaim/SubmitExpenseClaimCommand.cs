using MediatR;

namespace HRMS.Services.Expense.Application.Commands.SubmitExpenseClaim;

public class SubmitExpenseClaimCommand : IRequest
{
    public Guid Id { get; set; }
}
