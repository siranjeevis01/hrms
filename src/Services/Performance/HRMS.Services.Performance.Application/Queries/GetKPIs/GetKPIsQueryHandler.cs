using AutoMapper;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Performance.Application.Queries.GetKPIs;

public class GetKPIsQueryHandler : IRequestHandler<GetKPIsQuery, PagedResult<KPIDto>>
{
    private readonly IPerformanceDbContext _context;
    private readonly IMapper _mapper;

    public GetKPIsQueryHandler(IPerformanceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<KPIDto>> Handle(GetKPIsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.KPIs
            .Where(k => k.TenantId == request.TenantId)
            .AsQueryable();

        if (request.Category.HasValue)
            query = query.Where(k => k.Category == request.Category.Value);

        if (!string.IsNullOrWhiteSpace(request.Period))
            query = query.Where(k => k.Period == request.Period);

        if (request.DepartmentId.HasValue)
            query = query.Where(k => k.DepartmentId == request.DepartmentId.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var kpis = await query
            .OrderByDescending(k => k.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = _mapper.Map<List<KPIDto>>(kpis);

        return new PagedResult<KPIDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
