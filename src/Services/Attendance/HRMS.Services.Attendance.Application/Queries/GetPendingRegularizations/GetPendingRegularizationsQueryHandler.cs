using AutoMapper;
using HRMS.Services.Attendance.Application.DTOs;
using HRMS.Services.Attendance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.GetPendingRegularizations;

public class GetPendingRegularizationsQueryHandler : IRequestHandler<GetPendingRegularizationsQuery, List<RegularizationDto>>
{
    private readonly IAttendanceRepository _repository;
    private readonly IMapper _mapper;

    public GetPendingRegularizationsQueryHandler(IAttendanceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<RegularizationDto>> Handle(GetPendingRegularizationsQuery request, CancellationToken cancellationToken)
    {
        var records = await _repository.GetPendingRegularizationsAsync(request.TenantId, cancellationToken);
        return _mapper.Map<List<RegularizationDto>>(records);
    }
}
