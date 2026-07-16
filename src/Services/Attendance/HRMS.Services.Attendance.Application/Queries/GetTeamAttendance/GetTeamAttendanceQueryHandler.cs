using AutoMapper;
using HRMS.Services.Attendance.Application.DTOs;
using HRMS.Services.Attendance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.GetTeamAttendance;

public class GetTeamAttendanceQueryHandler : IRequestHandler<GetTeamAttendanceQuery, List<AttendanceRecordDto>>
{
    private readonly IAttendanceRepository _repository;
    private readonly IMapper _mapper;

    public GetTeamAttendanceQueryHandler(IAttendanceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<AttendanceRecordDto>> Handle(GetTeamAttendanceQuery request, CancellationToken cancellationToken)
    {
        var records = await _repository.GetTeamAttendanceAsync(
            request.ManagerId, request.Date, cancellationToken);

        return _mapper.Map<List<AttendanceRecordDto>>(records);
    }
}
