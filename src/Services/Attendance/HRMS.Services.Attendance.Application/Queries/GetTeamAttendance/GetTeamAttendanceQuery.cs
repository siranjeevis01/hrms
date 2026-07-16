using HRMS.Services.Attendance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.GetTeamAttendance;

public class GetTeamAttendanceQuery : IRequest<List<AttendanceRecordDto>>
{
    public Guid ManagerId { get; set; }
    public DateTime Date { get; set; }
}
