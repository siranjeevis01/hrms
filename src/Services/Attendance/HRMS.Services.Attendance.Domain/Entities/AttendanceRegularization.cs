using HRMS.Services.Attendance.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Attendance.Domain.Entities;

public class AttendanceRegularization : BaseEntity
{
    public Guid AttendanceRecordId { get; private set; }
    public Guid EmployeeId { get; private set; }
    public string Reason { get; private set; } = string.Empty;
    public DateTime RequestedDate { get; private set; }
    public DateTime? OriginalCheckIn { get; private set; }
    public DateTime? OriginalCheckOut { get; private set; }
    public DateTime? RequestedCheckIn { get; private set; }
    public DateTime? RequestedCheckOut { get; private set; }
    public RegularizationStatus Status { get; private set; }
    public Guid? ApprovedBy { get; private set; }
    public DateTime? ApprovedAt { get; private set; }
    public string? RejectionReason { get; private set; }

    private AttendanceRegularization() { }

    public static AttendanceRegularization Create(
        Guid attendanceRecordId,
        Guid employeeId,
        string reason,
        DateTime requestedDate,
        DateTime? originalCheckIn,
        DateTime? originalCheckOut,
        DateTime? requestedCheckIn,
        DateTime? requestedCheckOut,
        Guid? tenantId = null)
    {
        var regularization = new AttendanceRegularization
        {
            AttendanceRecordId = attendanceRecordId,
            EmployeeId = employeeId,
            Reason = reason,
            RequestedDate = requestedDate.Date,
            OriginalCheckIn = originalCheckIn,
            OriginalCheckOut = originalCheckOut,
            RequestedCheckIn = requestedCheckIn,
            RequestedCheckOut = requestedCheckOut,
            Status = RegularizationStatus.Pending
        };

        if (tenantId.HasValue)
            regularization.TenantId = tenantId.Value;

        return regularization;
    }

    public void Approve(Guid approvedBy)
    {
        Status = RegularizationStatus.Approved;
        ApprovedBy = approvedBy;
        ApprovedAt = DateTime.UtcNow;
    }

    public void Reject(Guid approvedBy, string? rejectionReason = null)
    {
        Status = RegularizationStatus.Rejected;
        ApprovedBy = approvedBy;
        ApprovedAt = DateTime.UtcNow;
        RejectionReason = rejectionReason;
    }
}
