using HRMS.Services.Expense.Domain.Enums;

namespace HRMS.Services.Expense.Application.DTOs;

public class ExpenseApprovalDto
{
    public Guid Id { get; set; }
    public Guid ClaimId { get; set; }
    public Guid ApproverId { get; set; }
    public ApprovalLevel Level { get; set; }
    public ApprovalStatus Status { get; set; }
    public string? Comments { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
