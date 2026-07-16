using HRMS.Services.Audit.Application.Interfaces;
using HRMS.Services.Audit.Domain.Entities;
using MediatR;

namespace HRMS.Services.Audit.Application.Commands.LogAuditEntry;

public class LogAuditEntryCommandHandler : IRequestHandler<LogAuditEntryCommand, Guid>
{
    private readonly IAuditDbContext _context;

    public LogAuditEntryCommandHandler(IAuditDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(LogAuditEntryCommand request, CancellationToken cancellationToken)
    {
        var auditLog = AuditLog.Create(
            request.UserId,
            request.UserName,
            request.ActionType,
            request.EntityType,
            request.EntityId,
            request.OldValues,
            request.NewValues,
            request.IpAddress,
            request.UserAgent,
            request.TenantId);

        if (request.Trails != null)
        {
            foreach (var trail in request.Trails)
            {
                auditLog.AddTrail(AuditTrail.Create(
                    auditLog.Id,
                    trail.FieldName,
                    trail.OldValue,
                    trail.NewValue,
                    request.TenantId));
            }
        }

        _context.AuditLogs.Add(auditLog);
        await _context.SaveChangesAsync(cancellationToken);

        return auditLog.Id;
    }
}
