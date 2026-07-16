using AutoMapper;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Performance.Application.Queries.GetOKRs;

public class GetOKRsQueryHandler : IRequestHandler<GetOKRsQuery, PagedResult<OKRDto>>
{
    private readonly IPerformanceDbContext _context;
    private readonly IMapper _mapper;

    public GetOKRsQueryHandler(IPerformanceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<OKRDto>> Handle(GetOKRsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.OKRs
            .Where(o => o.TenantId == request.TenantId)
            .AsQueryable();

        if (request.Status.HasValue)
            query = query.Where(o => o.Status == request.Status.Value);

        if (!string.IsNullOrWhiteSpace(request.Period))
            query = query.Where(o => o.Period == request.Period);

        var totalCount = await query.CountAsync(cancellationToken);

        var okrs = await query
            .OrderByDescending(o => o.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = _mapper.Map<List<OKRDto>>(okrs);

        return new PagedResult<OKRDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
