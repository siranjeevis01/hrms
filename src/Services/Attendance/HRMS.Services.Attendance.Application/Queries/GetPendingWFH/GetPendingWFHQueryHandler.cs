using AutoMapper;
using HRMS.Services.Attendance.Application.DTOs;
using HRMS.Services.Attendance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.GetPendingWFH;

public class GetPendingWFHQueryHandler : IRequestHandler<GetPendingWFHQuery, List<WorkFromHomeDto>>
{
    private readonly IAttendanceRepository _repository;
    private readonly IMapper _mapper;

    public GetPendingWFHQueryHandler(IAttendanceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<WorkFromHomeDto>> Handle(GetPendingWFHQuery request, CancellationToken cancellationToken)
    {
        var records = await _repository.GetPendingWFHAsync(request.TenantId, cancellationToken);
        return _mapper.Map<List<WorkFromHomeDto>>(records);
    }
}
