using AutoMapper;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace HRMS.Services.Performance.Application.Queries.GetCalibrationSession;

public class GetCalibrationSessionQueryHandler : IRequestHandler<GetCalibrationSessionQuery, CalibrationSessionDto?>
{
    private readonly IPerformanceDbContext _context;
    private readonly IMapper _mapper;

    public GetCalibrationSessionQueryHandler(IPerformanceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CalibrationSessionDto?> Handle(GetCalibrationSessionQuery request, CancellationToken cancellationToken)
    {
        var session = await _context.CalibrationSessions
            .Include(s => s.Entries)
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        if (session == null)
            return null;

        return _mapper.Map<CalibrationSessionDto>(session);
    }
}
