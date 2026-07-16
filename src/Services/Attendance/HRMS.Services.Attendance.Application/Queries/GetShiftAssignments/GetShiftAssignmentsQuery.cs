using HRMS.Services.Attendance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.GetShiftAssignments;

public class GetShiftAssignmentsQuery : IRequest<List<ShiftAssignmentDto>>
{
    public Guid? EmployeeId { get; set; }
}
