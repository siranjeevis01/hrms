using HRMS.Services.Attendance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.GetMonthlyAttendance;

public class GetMonthlyAttendanceQuery : IRequest<AttendanceSummaryDto?>
{
    public Guid EmployeeId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
}
