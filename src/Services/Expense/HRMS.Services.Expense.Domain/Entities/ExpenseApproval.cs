using HRMS.Services.Expense.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Expense.Domain.Entities;

public class ExpenseApproval : BaseEntity
{
    public Guid ClaimId { get; private set; }
    public Guid ApproverId { get; private set; }
    public ApprovalLevel Level { get; private set; }
    public new ApprovalStatus Status { get; private set; }
    public string? Comments { get; private set; }
    public DateTime? ApprovedAt { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private ExpenseApproval() { }

    public static ExpenseApproval Create(
        Guid claimId,
        Guid approverId,
        ApprovalLevel level,
        string? comments,
        string tenantId)
    {
        return new ExpenseApproval
        {
            Id = Guid.NewGuid(),
            ClaimId = claimId,
            ApproverId = approverId,
            Level = level,
            Status = ApprovalStatus.Pending,
            Comments = comments,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Approve(string? comments)
    {
        Status = ApprovalStatus.Approved;
        Comments = comments ?? Comments;
        ApprovedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reject(string? comments)
    {
        Status = ApprovalStatus.Rejected;
        Comments = comments ?? Comments;
        UpdatedAt = DateTime.UtcNow;
    }
}
