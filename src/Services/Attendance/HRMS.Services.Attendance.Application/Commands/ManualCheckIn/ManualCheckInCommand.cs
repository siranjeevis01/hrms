using HRMS.Services.Attendance.Domain.Enums;
using MediatR;

namespace HRMS.Services.Attendance.Application.Commands.ManualCheckIn;

public class ManualCheckInCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public DateTime? CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public Guid? ShiftId { get; set; }
    public AttendanceStatus Status { get; set; } = AttendanceStatus.Present;
    public string? Notes { get; set; }
    public Guid? TenantId { get; set; }
}
