using MediatR;

namespace HRMS.Services.Attendance.Application.Commands.BulkMarkAttendance;

public class BulkMarkAttendanceCommand : IRequest<int>
{
    public List<BulkAttendanceEntry> Entries { get; set; } = new();
    public Guid? TenantId { get; set; }
}

public class BulkAttendanceEntry
{
    public Guid EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public Domain.Enums.AttendanceStatus Status { get; set; }
    public string? Notes { get; set; }
}
