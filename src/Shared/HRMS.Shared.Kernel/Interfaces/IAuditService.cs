using HRMS.Shared.Kernel.Common;
using HRMS.Shared.Kernel.Enums;

namespace HRMS.Shared.Kernel.Interfaces;

public interface IAuditService
{
    Task LogAsync(
        AuditAction action,
        string entityType,
        Guid entityId,
        string? performedBy = null,
        string? details = null,
        string? previousValues = null,
        string? newValue = null,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AuditEntry>> GetAuditTrailAsync(
        string entityType,
        Guid entityId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AuditEntry>> GetRecentAuditsAsync(
        int count = 50,
        CancellationToken cancellationToken = default);
}
