using MediatR;

namespace HRMS.Services.Attendance.Application.Commands.RequestWorkFromHome;

public class RequestWorkFromHomeCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; } = string.Empty;
    public Guid? TenantId { get; set; }
}
