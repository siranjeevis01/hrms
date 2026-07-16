using HRMS.Services.Identity.Domain.Entities;
using HRMS.Services.Identity.Infrastructure.Persistence;
using HRMS.Shared.Kernel.Common;
using HRMS.Shared.Kernel.Enums;
using HRMS.Shared.Kernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Identity.Infrastructure.Services;

public class AuditLogService : IAuditService
{
    private readonly IdentityDbContext _context;

    public AuditLogService(IdentityDbContext context)
    {
        _context = context;
    }

    public async Task LogAsync(
        AuditAction action,
        string entityType,
        Guid entityId,
        string? performedBy = null,
        string? details = null,
        string? previousValues = null,
        string? newValue = null,
        CancellationToken cancellationToken = default)
    {
        var auditLog = AuditLog.Create(
            Guid.NewGuid(),
            action.ToString(),
            userId: null,
            details: $"Entity: {entityType}, EntityId: {entityId}. {details}",
            isSuccess: true);

        _context.AuditLogs.Add(auditLog);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task LogAsync(
        string action,
        Guid? userId = null,
        string? ipAddress = null,
        string? userAgent = null,
        string? details = null,
        bool isSuccess = true,
        string? failureReason = null,
        CancellationToken cancellationToken = default)
    {
        var auditLog = AuditLog.Create(
            Guid.NewGuid(),
            action,
            userId,
            ipAddress,
            userAgent,
            details,
            isSuccess,
            failureReason);

        _context.AuditLogs.Add(auditLog);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<AuditEntry>> GetAuditTrailAsync(
        string entityType,
        Guid entityId,
        CancellationToken cancellationToken = default)
    {
        var logs = await _context.AuditLogs
            .AsNoTracking()
            .Where(a => a.Details != null && a.Details.Contains(entityType) && a.Details.Contains(entityId.ToString()))
            .OrderByDescending(a => a.Timestamp)
            .ToListAsync(cancellationToken);

        return logs.Select(l => new AuditEntry
        {
            Id = l.Id,
            Action = Enum.TryParse<AuditAction>(l.Action, out var action) ? action : AuditAction.View,
            PerformedBy = l.UserId?.ToString(),
            PerformedAt = l.Timestamp,
            Details = l.Details,
            EntityId = entityId,
            EntityType = entityType
        }).ToList();
    }

    public async Task<IReadOnlyList<AuditEntry>> GetRecentAuditsAsync(
        int count = 50,
        CancellationToken cancellationToken = default)
    {
        var logs = await _context.AuditLogs
            .AsNoTracking()
            .OrderByDescending(a => a.Timestamp)
            .Take(count)
            .ToListAsync(cancellationToken);

        return logs.Select(l => new AuditEntry
        {
            Id = l.Id,
            Action = Enum.TryParse<AuditAction>(l.Action, out var action) ? action : AuditAction.View,
            PerformedBy = l.UserId?.ToString(),
            PerformedAt = l.Timestamp,
            Details = l.Details,
            EntityId = l.UserId ?? Guid.Empty,
            EntityType = "AuditLog"
        }).ToList();
    }
}
