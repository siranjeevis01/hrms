using AutoMapper;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Performance.Application.Queries.GetEmployeeKPIs;

public class GetEmployeeKPIsQueryHandler : IRequestHandler<GetEmployeeKPIsQuery, List<KPIDto>>
{
    private readonly IPerformanceDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeKPIsQueryHandler(IPerformanceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<KPIDto>> Handle(GetEmployeeKPIsQuery request, CancellationToken cancellationToken)
    {
        var kpis = await _context.KPIs
            .Where(k => k.EmployeeId == request.EmployeeId && k.TenantId == request.TenantId)
            .OrderByDescending(k => k.CreatedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<KPIDto>>(kpis);
    }
}
