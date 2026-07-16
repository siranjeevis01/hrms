using HRMS.Services.Audit.Domain.Enums;
using MediatR;

namespace HRMS.Services.Audit.Application.Commands.LogDataChange;

public class LogDataChangeCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public AuditEntityType EntityType { get; set; }
    public string EntityId { get; set; } = string.Empty;
    public string ChangeType { get; set; } = string.Empty;
    public string? SerializedData { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
