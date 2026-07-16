using HRMS.Services.Document.Application.Interfaces;
using HRMS.Services.Document.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Document.Application.Commands.ShareDocument;

public class ShareDocumentCommand : IRequest
{
    public Guid DocumentId { get; set; }
    public Guid SharedWithUserId { get; set; }
    public DocumentPermission Permission { get; set; }
    public Guid SharedBy { get; set; }
    public DateTime? ExpiresAt { get; set; }
}

public class ShareDocumentCommandHandler : IRequestHandler<ShareDocumentCommand>
{
    private readonly IDocumentDbContext _context;

    public ShareDocumentCommandHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ShareDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await _context.Documents
            .FirstOrDefaultAsync(d => d.Id == request.DocumentId && !d.IsDeleted, cancellationToken);

        if (document == null)
            throw new InvalidOperationException($"Document with ID {request.DocumentId} not found.");

        document.Share(
            request.SharedWithUserId,
            request.Permission,
            request.SharedBy,
            request.ExpiresAt);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
