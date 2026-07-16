using HRMS.Services.Audit.Application.DTOs;
using HRMS.Services.Audit.Application.Interfaces;
using HRMS.Services.Audit.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Audit.Application.Queries.GetAuditStats;

public class GetAuditStatsQueryHandler : IRequestHandler<GetAuditStatsQuery, AuditStatsDto>
{
    private readonly IAuditDbContext _context;

    public GetAuditStatsQueryHandler(IAuditDbContext context)
    {
        _context = context;
    }

    public async Task<AuditStatsDto> Handle(GetAuditStatsQuery request, CancellationToken cancellationToken)
    {
        var auditQuery = _context.AuditLogs
            .Where(a => a.TenantId == request.TenantId)
            .AsQueryable();

        var loginQuery = _context.LoginHistories
            .Where(l => l.TenantId == request.TenantId)
            .AsQueryable();

        var dataChangeQuery = _context.DataChangeLogs
            .Where(d => d.TenantId == request.TenantId)
            .AsQueryable();

        if (request.FromDate.HasValue)
        {
            auditQuery = auditQuery.Where(a => a.Timestamp >= request.FromDate.Value);
            loginQuery = loginQuery.Where(l => l.LoginAt >= request.FromDate.Value);
            dataChangeQuery = dataChangeQuery.Where(d => d.Timestamp >= request.FromDate.Value);
        }

        if (request.ToDate.HasValue)
        {
            auditQuery = auditQuery.Where(a => a.Timestamp <= request.ToDate.Value);
            loginQuery = loginQuery.Where(l => l.LoginAt <= request.ToDate.Value);
            dataChangeQuery = dataChangeQuery.Where(d => d.Timestamp <= request.ToDate.Value);
        }

        var totalAuditLogs = await auditQuery.CountAsync(cancellationToken);
        var totalLogins = await loginQuery.CountAsync(cancellationToken);
        var successfulLogins = await loginQuery.Where(l => l.IsSuccessful).CountAsync(cancellationToken);
        var totalDataChanges = await dataChangeQuery.CountAsync(cancellationToken);

        var actionsByType = await auditQuery
            .GroupBy(a => a.ActionType)
            .Select(g => new { g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.Key, x => x.Count, cancellationToken);

        var changesByEntity = await auditQuery
            .GroupBy(a => a.EntityType)
            .Select(g => new { g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.Key, x => x.Count, cancellationToken);

        var recentActivity = await auditQuery
            .OrderByDescending(a => a.Timestamp)
            .Take(10)
            .Select(a => new AuditActivityDto
            {
                Action = a.ActionType.ToString(),
                Entity = a.EntityType.ToString(),
                UserName = a.UserName,
                Timestamp = a.Timestamp
            })
            .ToListAsync(cancellationToken);

        return new AuditStatsDto
        {
            TotalAuditLogs = totalAuditLogs,
            TotalLoginAttempts = totalLogins,
            SuccessfulLogins = successfulLogins,
            FailedLogins = totalLogins - successfulLogins,
            TotalDataChanges = totalDataChanges,
            ActionsByType = actionsByType,
            ChangesByEntity = changesByEntity,
            RecentActivity = recentActivity
        };
    }
}
