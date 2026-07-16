using HRMS.Services.Travel.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Travel.Domain.Entities;

public class TravelApproval : BaseEntity
{
    public Guid TravelRequestId { get; private set; }
    public Guid ApproverId { get; private set; }
    public TravelApprovalLevel Level { get; private set; }
    public ApprovalStatus Status { get; private set; }
    public string? Comments { get; private set; }
    public DateTime? ApprovedAt { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private TravelApproval() { }

    public static TravelApproval Create(
        Guid travelRequestId,
        Guid approverId,
        TravelApprovalLevel level,
        string? comments,
        string tenantId)
    {
        return new TravelApproval
        {
            Id = Guid.NewGuid(),
            TravelRequestId = travelRequestId,
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
