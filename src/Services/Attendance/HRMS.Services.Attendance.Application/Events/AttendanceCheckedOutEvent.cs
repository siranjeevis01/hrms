using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Attendance.Application.Events;

public class AttendanceCheckedOutEvent : DomainEvent
{
    public Guid EmployeeId { get; }
    public Guid AttendanceRecordId { get; }
    public DateTime CheckOutTime { get; }
    public decimal? TotalHours { get; }

    public AttendanceCheckedOutEvent(Guid employeeId, Guid attendanceRecordId, DateTime checkOutTime, decimal? totalHours)
        : base("AttendanceCheckedOut")
    {
        EmployeeId = employeeId;
        AttendanceRecordId = attendanceRecordId;
        CheckOutTime = checkOutTime;
        TotalHours = totalHours;
    }
}
