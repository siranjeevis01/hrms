using HRMS.Services.Attendance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.GetEmployeeAttendance;

public class GetEmployeeAttendanceQuery : IRequest<List<AttendanceRecordDto>>
{
    public Guid EmployeeId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}
