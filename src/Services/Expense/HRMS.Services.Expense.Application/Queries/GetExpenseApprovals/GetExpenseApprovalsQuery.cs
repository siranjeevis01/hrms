using HRMS.Services.Expense.Application.DTOs;
using HRMS.Services.Expense.Domain.Enums;
using MediatR;

namespace HRMS.Services.Expense.Application.Queries.GetExpenseApprovals;

public class GetExpenseApprovalsQuery : IRequest<List<ExpenseApprovalDto>>
{
    public Guid? ApproverId { get; set; }
    public ApprovalStatus? Status { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
