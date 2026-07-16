using AutoMapper;
using HRMS.Services.Attendance.Application.DTOs;
using HRMS.Services.Attendance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.GetGeoFences;

public class GetGeoFencesQueryHandler : IRequestHandler<GetGeoFencesQuery, List<GeoFenceDto>>
{
    private readonly IAttendanceRepository _repository;
    private readonly IMapper _mapper;

    public GetGeoFencesQueryHandler(IAttendanceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GeoFenceDto>> Handle(GetGeoFencesQuery request, CancellationToken cancellationToken)
    {
        var fences = await _repository.GetGeoFencesAsync(request.CompanyId, cancellationToken);
        return _mapper.Map<List<GeoFenceDto>>(fences);
    }
}
