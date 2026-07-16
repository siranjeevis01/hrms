using AutoMapper;
using HRMS.Services.Attendance.Application.DTOs;
using HRMS.Services.Attendance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.GetAttendanceSummary;

public class GetAttendanceSummaryQueryHandler : IRequestHandler<GetAttendanceSummaryQuery, List<AttendanceSummaryDto>>
{
    private readonly IAttendanceRepository _repository;
    private readonly IMapper _mapper;

    public GetAttendanceSummaryQueryHandler(IAttendanceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<AttendanceSummaryDto>> Handle(GetAttendanceSummaryQuery request, CancellationToken cancellationToken)
    {
        var summaries = await _repository.GetAttendanceSummaryAsync(request.EmployeeId, cancellationToken);
        return _mapper.Map<List<AttendanceSummaryDto>>(summaries);
    }
}
