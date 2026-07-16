using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Leave.Domain.Entities;

public enum ApproverType
{
    ReportingManager,
    HR,
    DepartmentHead,
    SkipLevel
}

public class LeaveApprovalMatrix : BaseEntity
{
    private LeaveApprovalMatrix() { }

    public Guid LeaveTypeId { get; private set; }
    public Guid CompanyId { get; private set; }
    public int Level { get; private set; }
    public ApproverType ApproverType { get; private set; }
    public Guid? ApproverUserId { get; private set; }
    public bool IsRequired { get; private set; }
    public Guid TenantId { get; private set; }

    public static LeaveApprovalMatrix Create(
        Guid id,
        Guid leaveTypeId,
        Guid companyId,
        int level,
        ApproverType approverType,
        Guid? approverUserId,
        bool isRequired,
        Guid tenantId)
    {
        return new LeaveApprovalMatrix
        {
            Id = id,
            LeaveTypeId = leaveTypeId,
            CompanyId = companyId,
            Level = level,
            ApproverType = approverType,
            ApproverUserId = approverUserId,
            IsRequired = isRequired,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(int level, ApproverType approverType, Guid? approverUserId, bool isRequired)
    {
        Level = level;
        ApproverType = approverType;
        ApproverUserId = approverUserId;
        IsRequired = isRequired;
        UpdatedAt = DateTime.UtcNow;
    }
}
