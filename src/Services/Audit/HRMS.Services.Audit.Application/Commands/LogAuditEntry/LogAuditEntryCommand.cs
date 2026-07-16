using HRMS.Services.Audit.Domain.Enums;
using MediatR;

namespace HRMS.Services.Audit.Application.Commands.LogAuditEntry;

public class LogAuditEntryCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public AuditActionType ActionType { get; set; }
    public AuditEntityType EntityType { get; set; }
    public string EntityId { get; set; } = string.Empty;
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public List<LogAuditTrailItem>? Trails { get; set; }
}

public class LogAuditTrailItem
{
    public string FieldName { get; set; } = string.Empty;
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
}
