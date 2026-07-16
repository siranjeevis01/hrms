using HRMS.Services.Expense.Application.DTOs;
using HRMS.Services.Expense.Application.Queries.GetExpenseClaims;
using HRMS.Services.Expense.Domain.Enums;
using MediatR;

namespace HRMS.Services.Expense.Application.Queries.GetEmployeeExpenseClaims;

public class GetEmployeeExpenseClaimsQuery : IRequest<PagedResult<ExpenseClaimDto>>
{
    public Guid EmployeeId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public ClaimStatus? Status { get; set; }
}
