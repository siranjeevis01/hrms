using HRMS.Services.Document.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Document.Application.Commands.CreateDocumentVersion;

public class CreateDocumentVersionCommand : IRequest
{
    public Guid DocumentId { get; set; }
    public string FileUrl { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public Guid UploadedBy { get; set; }
    public string? ChangeNotes { get; set; }
}

public class CreateDocumentVersionCommandHandler : IRequestHandler<CreateDocumentVersionCommand>
{
    private readonly IDocumentDbContext _context;

    public CreateDocumentVersionCommandHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateDocumentVersionCommand request, CancellationToken cancellationToken)
    {
        var document = await _context.Documents
            .FirstOrDefaultAsync(d => d.Id == request.DocumentId && !d.IsDeleted, cancellationToken);

        if (document == null)
            throw new InvalidOperationException($"Document with ID {request.DocumentId} not found.");

        document.AddVersion(request.FileUrl, request.FileSize, request.UploadedBy, request.ChangeNotes);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
