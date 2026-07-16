using AutoMapper;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Performance.Application.Queries.GetPerformanceReviews;

public class GetPerformanceReviewsQueryHandler : IRequestHandler<GetPerformanceReviewsQuery, PagedResult<PerformanceReviewDto>>
{
    private readonly IPerformanceDbContext _context;
    private readonly IMapper _mapper;

    public GetPerformanceReviewsQueryHandler(IPerformanceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<PerformanceReviewDto>> Handle(GetPerformanceReviewsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.PerformanceReviews
            .Where(r => r.TenantId == request.TenantId)
            .AsQueryable();

        if (request.EmployeeId.HasValue)
            query = query.Where(r => r.EmployeeId == request.EmployeeId.Value);

        if (request.ReviewType.HasValue)
            query = query.Where(r => r.ReviewType == request.ReviewType.Value);

        if (request.Status.HasValue)
            query = query.Where(r => r.Status == request.Status.Value);

        if (!string.IsNullOrWhiteSpace(request.ReviewPeriod))
            query = query.Where(r => r.ReviewPeriod == request.ReviewPeriod);

        var totalCount = await query.CountAsync(cancellationToken);

        var reviews = await query
            .OrderByDescending(r => r.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = _mapper.Map<List<PerformanceReviewDto>>(reviews);

        return new PagedResult<PerformanceReviewDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
