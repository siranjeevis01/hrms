using AutoMapper;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Performance.Application.Queries.GetKPI;

public class GetKPIQueryHandler : IRequestHandler<GetKPIQuery, KPIDto?>
{
    private readonly IPerformanceDbContext _context;
    private readonly IMapper _mapper;

    public GetKPIQueryHandler(IPerformanceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<KPIDto?> Handle(GetKPIQuery request, CancellationToken cancellationToken)
    {
        var kpi = await _context.KPIs
            .FirstOrDefaultAsync(k => k.Id == request.Id, cancellationToken);

        if (kpi == null)
            return null;

        return _mapper.Map<KPIDto>(kpi);
    }
}
