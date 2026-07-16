using AutoMapper;
using HRMS.Services.Attendance.Application.DTOs;
using HRMS.Services.Attendance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.GetEmployeeAttendance;

public class GetEmployeeAttendanceQueryHandler : IRequestHandler<GetEmployeeAttendanceQuery, List<AttendanceRecordDto>>
{
    private readonly IAttendanceRepository _repository;
    private readonly IMapper _mapper;

    public GetEmployeeAttendanceQueryHandler(IAttendanceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<AttendanceRecordDto>> Handle(GetEmployeeAttendanceQuery request, CancellationToken cancellationToken)
    {
        var records = await _repository.GetByEmployeeDateRangeAsync(
            request.EmployeeId, request.FromDate, request.ToDate, cancellationToken);

        return _mapper.Map<List<AttendanceRecordDto>>(records);
    }
}
