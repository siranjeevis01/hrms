using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Attendance.Application.Events;

public class LateMarkDetectedEvent : DomainEvent
{
    public Guid EmployeeId { get; }
    public Guid AttendanceRecordId { get; }
    public int LateMinutes { get; }

    public LateMarkDetectedEvent(Guid employeeId, Guid attendanceRecordId, int lateMinutes)
        : base("LateMarkDetected")
    {
        EmployeeId = employeeId;
        AttendanceRecordId = attendanceRecordId;
        LateMinutes = lateMinutes;
    }
}
