using AutoMapper;
using HRMS.Services.Attendance.Application.DTOs;
using HRMS.Services.Attendance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.GetMonthlyAttendance;

public class GetMonthlyAttendanceQueryHandler : IRequestHandler<GetMonthlyAttendanceQuery, AttendanceSummaryDto?>
{
    private readonly IAttendanceRepository _repository;
    private readonly IMapper _mapper;

    public GetMonthlyAttendanceQueryHandler(IAttendanceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AttendanceSummaryDto?> Handle(GetMonthlyAttendanceQuery request, CancellationToken cancellationToken)
    {
        var summary = await _repository.GetMonthlySummaryAsync(
            request.EmployeeId, request.Year, request.Month, cancellationToken);

        return summary == null ? null : _mapper.Map<AttendanceSummaryDto>(summary);
    }
}
