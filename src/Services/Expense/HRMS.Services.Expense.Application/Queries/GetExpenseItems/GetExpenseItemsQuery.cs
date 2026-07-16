using HRMS.Services.Expense.Application.DTOs;
using MediatR;

namespace HRMS.Services.Expense.Application.Queries.GetExpenseItems;

public class GetExpenseItemsQuery : IRequest<List<ExpenseItemDto>>
{
    public Guid ClaimId { get; set; }
}
