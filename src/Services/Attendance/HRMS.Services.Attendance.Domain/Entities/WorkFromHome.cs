using HRMS.Services.Attendance.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Attendance.Domain.Entities;

public class WorkFromHome : BaseEntity
{
    public Guid EmployeeId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public string Reason { get; private set; } = string.Empty;
    public new AttendanceStatus Status { get; private set; }
    public Guid? ApprovedBy { get; private set; }
    public DateTime? ApprovedAt { get; private set; }
    public string? DayWiseStatus { get; private set; }

    private WorkFromHome() { }

    public static WorkFromHome Create(
        Guid employeeId,
        DateTime startDate,
        DateTime endDate,
        string reason,
        Guid? tenantId = null)
    {
        if (endDate < startDate)
            throw new ArgumentException("End date must be after start date.");

        var wfh = new WorkFromHome
        {
            EmployeeId = employeeId,
            StartDate = startDate.Date,
            EndDate = endDate.Date,
            Reason = reason,
            Status = AttendanceStatus.OnLeave
        };

        if (tenantId.HasValue)
            wfh.TenantId = tenantId.Value;

        return wfh;
    }

    public void Approve(Guid approvedBy)
    {
        Status = AttendanceStatus.WFH;
        ApprovedBy = approvedBy;
        ApprovedAt = DateTime.UtcNow;
    }

    public void Reject(Guid approvedBy)
    {
        Status = AttendanceStatus.Absent;
        ApprovedBy = approvedBy;
        ApprovedAt = DateTime.UtcNow;
    }

    public void UpdateDayWiseStatus(string jsonStatus)
    {
        DayWiseStatus = jsonStatus;
    }

    public bool CoversDate(DateTime date)
    {
        var dateOnly = date.Date;
        return dateOnly >= StartDate.Date && dateOnly <= EndDate.Date;
    }
}
