using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Attendance.Application.Events;

public class AttendanceCheckedInEvent : DomainEvent
{
    public Guid EmployeeId { get; }
    public Guid AttendanceRecordId { get; }
    public DateTime CheckInTime { get; }

    public AttendanceCheckedInEvent(Guid employeeId, Guid attendanceRecordId, DateTime checkInTime)
        : base("AttendanceCheckedIn")
    {
        EmployeeId = employeeId;
        AttendanceRecordId = attendanceRecordId;
        CheckInTime = checkInTime;
    }
}
