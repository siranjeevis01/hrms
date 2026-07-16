using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Leave.Application.DTOs;

public class LeaveApprovalMatrixDto : BaseDto
{
    public Guid LeaveTypeId { get; set; }
    public string? LeaveTypeName { get; set; }
    public Guid CompanyId { get; set; }
    public int Level { get; set; }
    public string ApproverType { get; set; } = string.Empty;
    public Guid? ApproverUserId { get; set; }
    public string? ApproverUserName { get; set; }
    public bool IsRequired { get; set; }
}
