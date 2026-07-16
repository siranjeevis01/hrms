using HRMS.Services.Expense.Application.DTOs;
using MediatR;

namespace HRMS.Services.Expense.Application.Queries.GetExpenseClaim;

public class GetExpenseClaimQuery : IRequest<ExpenseClaimDto?>
{
    public Guid Id { get; set; }
}
