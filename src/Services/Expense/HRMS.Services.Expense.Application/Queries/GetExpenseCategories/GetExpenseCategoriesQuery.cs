using HRMS.Services.Expense.Application.DTOs;
using MediatR;

namespace HRMS.Services.Expense.Application.Queries.GetExpenseCategories;

public class GetExpenseCategoriesQuery : IRequest<List<ExpenseCategoryDto>>
{
    public string TenantId { get; set; } = string.Empty;
}
