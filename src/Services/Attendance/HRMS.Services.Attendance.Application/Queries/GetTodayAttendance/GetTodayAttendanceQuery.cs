using HRMS.Services.Attendance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.GetTodayAttendance;

public class GetTodayAttendanceQuery : IRequest<TodayAttendanceDto?>
{
    public Guid EmployeeId { get; set; }
}
