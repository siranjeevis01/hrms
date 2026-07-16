using HRMS.Services.Document.Application.Interfaces;
using HRMS.Services.Document.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Document.Application.Commands.UpdateDocument;

public class UpdateDocumentCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Tags { get; set; }
    public bool? IsPublic { get; set; }
    public DocumentCategory? Category { get; set; }
}

public class UpdateDocumentCommandHandler : IRequestHandler<UpdateDocumentCommand>
{
    private readonly IDocumentDbContext _context;

    public UpdateDocumentCommandHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await _context.Documents
            .FirstOrDefaultAsync(d => d.Id == request.Id && !d.IsDeleted, cancellationToken);

        if (document == null)
            throw new InvalidOperationException($"Document with ID {request.Id} not found.");

        document.Update(
            request.Name,
            request.Description,
            request.Tags,
            request.IsPublic,
            request.Category);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
