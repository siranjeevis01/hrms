using HRMS.Services.Document.Application.Interfaces;
using HRMS.Services.Document.Domain.Enums;
using MediatR;
using DocEntities = HRMS.Services.Document.Domain.Entities;

namespace HRMS.Services.Document.Application.Commands.UploadDocument;

public class UploadDocumentCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string FileUrl { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public Guid? FolderId { get; set; }
    public Guid UploadedBy { get; set; }
    public string? Description { get; set; }
    public string? Tags { get; set; }
    public bool IsPublic { get; set; }
    public DocumentCategory Category { get; set; }
    public string TenantId { get; set; } = string.Empty;
}

public class UploadDocumentCommandHandler : IRequestHandler<UploadDocumentCommand, Guid>
{
    private readonly IDocumentDbContext _context;

    public UploadDocumentCommandHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = DocEntities.Document.Create(
            request.Name,
            request.FileName,
            request.ContentType,
            request.FileSize,
            request.FileUrl,
            request.ThumbnailUrl,
            request.FolderId,
            request.UploadedBy,
            request.Description,
            request.Tags,
            request.IsPublic,
            request.Category,
            request.TenantId);

        _context.Documents.Add(document);
        await _context.SaveChangesAsync(cancellationToken);

        return document.Id;
    }
}
