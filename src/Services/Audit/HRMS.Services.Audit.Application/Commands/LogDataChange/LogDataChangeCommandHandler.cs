using HRMS.Services.Audit.Application.Interfaces;
using HRMS.Services.Audit.Domain.Entities;
using MediatR;

namespace HRMS.Services.Audit.Application.Commands.LogDataChange;

public class LogDataChangeCommandHandler : IRequestHandler<LogDataChangeCommand, Guid>
{
    private readonly IAuditDbContext _context;

    public LogDataChangeCommandHandler(IAuditDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(LogDataChangeCommand request, CancellationToken cancellationToken)
    {
        var dataChangeLog = DataChangeLog.Create(
            request.UserId,
            request.EntityType,
            request.EntityId,
            request.ChangeType,
            request.SerializedData,
            request.TenantId);

        _context.DataChangeLogs.Add(dataChangeLog);
        await _context.SaveChangesAsync(cancellationToken);

        return dataChangeLog.Id;
    }
}
