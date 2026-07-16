using HRMS.Services.Travel.Domain.Enums;

namespace HRMS.Services.Travel.Application.DTOs;

public class TravelApprovalDto
{
    public Guid Id { get; set; }
    public Guid TravelRequestId { get; set; }
    public Guid ApproverId { get; set; }
    public TravelApprovalLevel Level { get; set; }
    public ApprovalStatus Status { get; set; }
    public string? Comments { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
