using AutoMapper;
using HRMS.Services.Dashboard.Application.DTOs;
using HRMS.Services.Dashboard.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Dashboard.Application.Queries.GetAnalytics;

public class GetAnalyticsQueryHandler : IRequestHandler<GetAnalyticsQuery, List<AnalyticsEventDto>>
{
    private readonly IDashboardDbContext _context;
    private readonly IMapper _mapper;

    public GetAnalyticsQueryHandler(IDashboardDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<AnalyticsEventDto>> Handle(GetAnalyticsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.AnalyticsEvents
            .Where(a => a.TenantId == request.TenantId)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.EntityType))
            query = query.Where(a => a.EntityType == request.EntityType);

        if (request.FromDate.HasValue)
            query = query.Where(a => a.Timestamp >= request.FromDate.Value);

        if (request.ToDate.HasValue)
            query = query.Where(a => a.Timestamp <= request.ToDate.Value);

        var events = await query
            .OrderByDescending(a => a.Timestamp)
            .Take(request.MaxResults)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<AnalyticsEventDto>>(events);
    }
}
