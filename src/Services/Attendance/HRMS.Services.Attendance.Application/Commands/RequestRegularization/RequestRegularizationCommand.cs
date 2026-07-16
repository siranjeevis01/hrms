using MediatR;

namespace HRMS.Services.Attendance.Application.Commands.RequestRegularization;

public class RequestRegularizationCommand : IRequest<Guid>
{
    public Guid AttendanceRecordId { get; set; }
    public Guid EmployeeId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime? RequestedCheckIn { get; set; }
    public DateTime? RequestedCheckOut { get; set; }
    public Guid? TenantId { get; set; }
}
