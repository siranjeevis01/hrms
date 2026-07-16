using HRMS.Services.Attendance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.GetLateComers;

public class GetLateComersQuery : IRequest<List<AttendanceRecordDto>>
{
    public DateTime Date { get; set; }
    public Guid? DepartmentId { get; set; }
}
