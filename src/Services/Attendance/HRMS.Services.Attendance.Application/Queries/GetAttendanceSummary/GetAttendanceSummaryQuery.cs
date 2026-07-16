using HRMS.Services.Attendance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.GetAttendanceSummary;

public class GetAttendanceSummaryQuery : IRequest<List<AttendanceSummaryDto>>
{
    public Guid EmployeeId { get; set; }
}
