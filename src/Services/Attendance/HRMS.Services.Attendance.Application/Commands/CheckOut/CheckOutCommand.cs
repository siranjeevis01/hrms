using HRMS.Services.Attendance.Domain.Enums;
using MediatR;

namespace HRMS.Services.Attendance.Application.Commands.CheckOut;

public class CheckOutCommand : IRequest<Unit>
{
    public Guid EmployeeId { get; set; }
    public CheckInMethod Method { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}
