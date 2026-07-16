using HRMS.Services.Attendance.Domain.Enums;
using MediatR;

namespace HRMS.Services.Attendance.Application.Commands.CheckIn;

public class CheckInCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public CheckInMethod Method { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? WifiSSID { get; set; }
    public string? WifiBSSID { get; set; }
    public string? QrCodeId { get; set; }
    public Guid? TenantId { get; set; }
}
