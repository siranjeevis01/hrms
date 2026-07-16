using AutoMapper;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Performance.Application.Queries.GetCalibrationEntries;

public class GetCalibrationEntriesQueryHandler : IRequestHandler<GetCalibrationEntriesQuery, List<CalibrationEntryDto>>
{
    private readonly IPerformanceDbContext _context;
    private readonly IMapper _mapper;

    public GetCalibrationEntriesQueryHandler(IPerformanceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CalibrationEntryDto>> Handle(GetCalibrationEntriesQuery request, CancellationToken cancellationToken)
    {
        var entries = await _context.CalibrationEntries
            .Where(e => e.CalibrationSessionId == request.SessionId)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<CalibrationEntryDto>>(entries);
    }
}
