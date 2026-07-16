using AutoMapper;
using HRMS.Services.Attendance.Application.DTOs;
using HRMS.Services.Attendance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.GetShiftAssignments;

public class GetShiftAssignmentsQueryHandler : IRequestHandler<GetShiftAssignmentsQuery, List<ShiftAssignmentDto>>
{
    private readonly IAttendanceRepository _repository;
    private readonly IMapper _mapper;

    public GetShiftAssignmentsQueryHandler(IAttendanceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ShiftAssignmentDto>> Handle(GetShiftAssignmentsQuery request, CancellationToken cancellationToken)
    {
        var assignments = await _repository.GetShiftAssignmentsAsync(request.EmployeeId, cancellationToken);
        return _mapper.Map<List<ShiftAssignmentDto>>(assignments);
    }
}
