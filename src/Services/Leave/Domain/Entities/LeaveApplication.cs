using HRMS.Services.Leave.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Leave.Domain.Entities;

public class LeaveApplication : AggregateRoot
{
    private readonly List<LeaveComment> _comments = new();
    private LeaveApplication() { }

    public Guid EmployeeId { get; private set; }
    public Guid LeaveTypeId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public decimal TotalDays { get; private set; }
    public bool IsHalfDay { get; private set; }
    public HalfDayType? HalfDayType { get; private set; }
    public string Reason { get; private set; } = string.Empty;
    public new LeaveStatus Status { get; private set; }
    public DateTime AppliedAt { get; private set; }
    public Guid? ApprovedBy { get; private set; }
    public DateTime? ApprovedAt { get; private set; }
    public Guid? RejectedBy { get; private set; }
    public DateTime? RejectedAt { get; private set; }
    public string? RejectionReason { get; private set; }
    public int? CurrentApprovalLevel { get; private set; }
    public IReadOnlyList<LeaveComment> Comments => _comments.AsReadOnly();
    public bool IsSandwichApplied { get; private set; }
    public new Guid TenantId { get; private set; }

    public static LeaveApplication Create(
        Guid id,
        Guid employeeId,
        Guid leaveTypeId,
        DateTime startDate,
        DateTime endDate,
        decimal totalDays,
        bool isHalfDay,
        HalfDayType? halfDayType,
        string reason,
        bool isSandwichApplied,
        Guid tenantId)
    {
        return new LeaveApplication
        {
            Id = id,
            EmployeeId = employeeId,
            LeaveTypeId = leaveTypeId,
            StartDate = startDate,
            EndDate = endDate,
            TotalDays = totalDays,
            IsHalfDay = isHalfDay,
            HalfDayType = halfDayType,
            Reason = reason,
            Status = LeaveStatus.Draft,
            AppliedAt = DateTime.UtcNow,
            CurrentApprovalLevel = 1,
            IsSandwichApplied = isSandwichApplied,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Submit()
    {
        if (Status != LeaveStatus.Draft)
            throw new InvalidOperationException("Only draft applications can be submitted.");
        Status = LeaveStatus.Pending;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Approve(Guid approvedBy, int? nextLevel = null)
    {
        if (Status != LeaveStatus.Pending)
            throw new InvalidOperationException("Only pending applications can be approved.");
        UpdatedAt = DateTime.UtcNow;

        if (nextLevel.HasValue)
        {
            CurrentApprovalLevel = nextLevel.Value;
            return;
        }

        Status = LeaveStatus.Approved;
        ApprovedBy = approvedBy;
        ApprovedAt = DateTime.UtcNow;
        CurrentApprovalLevel = null;
    }

    public void Reject(Guid rejectedBy, string? reason)
    {
        if (Status != LeaveStatus.Pending)
            throw new InvalidOperationException("Only pending applications can be rejected.");
        Status = LeaveStatus.Rejected;
        RejectedBy = rejectedBy;
        RejectedAt = DateTime.UtcNow;
        RejectionReason = reason;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (Status != LeaveStatus.Pending && Status != LeaveStatus.Approved)
            throw new InvalidOperationException("Only pending or approved applications can be cancelled.");
        Status = LeaveStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Expire()
    {
        if (Status != LeaveStatus.Pending)
            throw new InvalidOperationException("Only pending applications can be expired.");
        Status = LeaveStatus.Expired;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddComment(Guid userId, string comment)
    {
        _comments.Add(LeaveComment.Create(Guid.NewGuid(), Id, userId, comment));
        UpdatedAt = DateTime.UtcNow;
    }
}
