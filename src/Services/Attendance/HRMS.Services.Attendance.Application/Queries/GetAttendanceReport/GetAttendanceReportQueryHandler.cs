using HRMS.Services.Attendance.Application.DTOs;
using HRMS.Services.Attendance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.GetAttendanceReport;

public class GetAttendanceReportQueryHandler : IRequestHandler<GetAttendanceReportQuery, List<AttendanceReportDto>>
{
    private readonly IAttendanceRepository _repository;

    public GetAttendanceReportQueryHandler(IAttendanceRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<AttendanceReportDto>> Handle(GetAttendanceReportQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAttendanceReportAsync(
            request.DepartmentId, request.FromDate, request.ToDate, cancellationToken);
    }
}
