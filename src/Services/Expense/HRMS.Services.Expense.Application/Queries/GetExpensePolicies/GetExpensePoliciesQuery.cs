using HRMS.Services.Expense.Application.DTOs;
using MediatR;

namespace HRMS.Services.Expense.Application.Queries.GetExpensePolicies;

public class GetExpensePoliciesQuery : IRequest<List<ExpensePolicyDto>>
{
    public string TenantId { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
}
