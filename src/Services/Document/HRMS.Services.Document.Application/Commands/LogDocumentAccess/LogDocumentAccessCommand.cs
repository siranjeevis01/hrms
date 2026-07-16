using HRMS.Services.Document.Application.Interfaces;
using HRMS.Services.Document.Domain.Enums;
using MediatR;
using DocEntities = HRMS.Services.Document.Domain.Entities;

namespace HRMS.Services.Document.Application.Commands.LogDocumentAccess;

public class LogDocumentAccessCommand : IRequest<Guid>
{
    public Guid DocumentId { get; set; }
    public Guid EmployeeId { get; set; }
    public DocumentAccessAction Action { get; set; }
    public string? IpAddress { get; set; }
    public string TenantId { get; set; } = string.Empty;
}

public class LogDocumentAccessCommandHandler : IRequestHandler<LogDocumentAccessCommand, Guid>
{
    private readonly IDocumentDbContext _context;

    public LogDocumentAccessCommandHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(LogDocumentAccessCommand request, CancellationToken cancellationToken)
    {
        var accessLog = DocEntities.DocumentAccessLog.Create(
            request.DocumentId,
            request.EmployeeId,
            request.Action,
            request.IpAddress,
            request.TenantId);

        _context.DocumentAccessLogs.Add(accessLog);
        await _context.SaveChangesAsync(cancellationToken);

        return accessLog.Id;
    }
}
