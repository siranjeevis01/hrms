using AutoMapper;
using HRMS.Services.Attendance.Application.DTOs;
using HRMS.Services.Attendance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.GetLateComers;

public class GetLateComersQueryHandler : IRequestHandler<GetLateComersQuery, List<AttendanceRecordDto>>
{
    private readonly IAttendanceRepository _repository;
    private readonly IMapper _mapper;

    public GetLateComersQueryHandler(IAttendanceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<AttendanceRecordDto>> Handle(GetLateComersQuery request, CancellationToken cancellationToken)
    {
        var records = await _repository.GetLateComersAsync(
            request.Date, request.DepartmentId, cancellationToken);

        return _mapper.Map<List<AttendanceRecordDto>>(records);
    }
}
