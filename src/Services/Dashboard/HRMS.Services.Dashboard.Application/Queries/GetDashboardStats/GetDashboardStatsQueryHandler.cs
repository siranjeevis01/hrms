using HRMS.Services.Dashboard.Application.DTOs;
using HRMS.Services.Dashboard.Application.Interfaces;
using HRMS.Services.Dashboard.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Dashboard.Application.Queries.GetDashboardStats;

public class GetDashboardStatsQueryHandler : IRequestHandler<GetDashboardStatsQuery, DashboardStatsDto>
{
    private readonly IDashboardDbContext _context;

    public GetDashboardStatsQueryHandler(IDashboardDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardStatsDto> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
    {
        var totalDashboards = await _context.Dashboards
            .CountAsync(d => d.TenantId == request.TenantId, cancellationToken);

        var totalWidgets = await _context.DashboardWidgets
            .CountAsync(w => w.TenantId == request.TenantId, cancellationToken);

        var totalShares = await _context.DashboardShares
            .CountAsync(s => s.TenantId == request.TenantId, cancellationToken);

        var totalAnalyticsEvents = await _context.AnalyticsEvents
            .CountAsync(a => a.TenantId == request.TenantId, cancellationToken);

        var widgetsByType = await _context.DashboardWidgets
            .Where(w => w.TenantId == request.TenantId)
            .GroupBy(w => w.WidgetType)
            .Select(g => new { g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.Key, x => x.Count, cancellationToken);

        var recentEvents = await _context.AnalyticsEvents
            .Where(a => a.TenantId == request.TenantId)
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

        return new DashboardStatsDto
        {
            TotalDashboards = totalDashboards,
            TotalWidgets = totalWidgets,
            TotalShares = totalShares,
            TotalAnalyticsEvents = totalAnalyticsEvents,
            WidgetsByType = widgetsByType,
            RecentEvents = recentEvents
        };
    }
}
