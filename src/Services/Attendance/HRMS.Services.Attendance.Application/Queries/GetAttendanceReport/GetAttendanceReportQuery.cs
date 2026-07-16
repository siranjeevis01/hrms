using HRMS.Services.Attendance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.GetAttendanceReport;

public class GetAttendanceReportQuery : IRequest<List<AttendanceReportDto>>
{
    public Guid? DepartmentId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}
