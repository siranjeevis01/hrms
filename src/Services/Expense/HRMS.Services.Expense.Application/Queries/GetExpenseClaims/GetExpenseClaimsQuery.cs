using HRMS.Services.Expense.Application.DTOs;
using HRMS.Services.Expense.Domain.Enums;
using MediatR;

namespace HRMS.Services.Expense.Application.Queries.GetExpenseClaims;

public class GetExpenseClaimsQuery : IRequest<PagedResult<ExpenseClaimDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? EmployeeId { get; set; }
    public ClaimStatus? Status { get; set; }
    public ClaimCategory? Category { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? SearchTerm { get; set; }
}
