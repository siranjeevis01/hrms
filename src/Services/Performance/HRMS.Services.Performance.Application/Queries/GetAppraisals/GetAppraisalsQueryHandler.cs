using AutoMapper;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Performance.Application.Queries.GetAppraisals;

public class GetAppraisalsQueryHandler : IRequestHandler<GetAppraisalsQuery, PagedResult<AppraisalDto>>
{
    private readonly IPerformanceDbContext _context;
    private readonly IMapper _mapper;

    public GetAppraisalsQueryHandler(IPerformanceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<AppraisalDto>> Handle(GetAppraisalsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Appraisals
            .Where(a => a.TenantId == request.TenantId)
            .AsQueryable();

        if (request.Status.HasValue)
            query = query.Where(a => a.Status == request.Status.Value);

        if (request.Type.HasValue)
            query = query.Where(a => a.Type == request.Type.Value);

        if (!string.IsNullOrWhiteSpace(request.Period))
            query = query.Where(a => a.Period == request.Period);

        var totalCount = await query.CountAsync(cancellationToken);

        var appraisals = await query
            .OrderByDescending(a => a.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = _mapper.Map<List<AppraisalDto>>(appraisals);

        return new PagedResult<AppraisalDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
