using HRMS.Services.Dashboard.Application.DTOs;
using HRMS.Services.Dashboard.Application.Interfaces;
using HRMS.Services.Dashboard.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Dashboard.Application.Queries.GetEmployeeAnalytics;

public class GetEmployeeAnalyticsQueryHandler : IRequestHandler<GetEmployeeAnalyticsQuery, EmployeeAnalyticsDto>
{
    private readonly IDashboardDbContext _context;

    public GetEmployeeAnalyticsQueryHandler(IDashboardDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeAnalyticsDto> Handle(GetEmployeeAnalyticsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.AnalyticsEvents
            .Where(a => a.EmployeeId == request.EmployeeId && a.TenantId == request.TenantId)
            .AsQueryable();

        if (request.FromDate.HasValue)
            query = query.Where(a => a.Timestamp >= request.FromDate.Value);

        if (request.ToDate.HasValue)
            query = query.Where(a => a.Timestamp <= request.ToDate.Value);

        var events = await query.ToListAsync(cancellationToken);

        var activityByEntity = events
            .Where(e => !string.IsNullOrWhiteSpace(e.EntityType))
            .GroupBy(e => e.EntityType!)
            .ToDictionary(g => g.Key, g => g.Count());

        var recentEvents = await query
            .OrderByDescending(a => a.Timestamp)
            .Take(10)
            .Select(a => new AnalyticsEventDto
            {
                Id = a.Id,
                EventType = a.EventType,
                EntityId = a.EntityId,
                EntityType = a.EntityType,
                EmployeeId = a.EmployeeId,
                Data = a.Data,
                Timestamp = a.Timestamp,
                TenantId = a.TenantId
            })
            .ToListAsync(cancellationToken);

        return new EmployeeAnalyticsDto
        {
            EmployeeId = request.EmployeeId,
            TotalEvents = events.Count,
            PageViews = events.Count(e => e.EventType == AnalyticsEventType.PageView),
            Actions = events.Count(e => e.EventType == AnalyticsEventType.Action),
            Filters = events.Count(e => e.EventType == AnalyticsEventType.Filter),
            Exports = events.Count(e => e.EventType == AnalyticsEventType.Export),
            LastActivityAt = events.Any() ? events.Max(e => e.Timestamp) : null,
            ActivityByEntity = activityByEntity,
            RecentEvents = recentEvents
        };
    }
}
