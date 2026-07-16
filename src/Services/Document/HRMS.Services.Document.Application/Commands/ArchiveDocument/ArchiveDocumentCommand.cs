using HRMS.Services.Document.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Document.Application.Commands.ArchiveDocument;

public class ArchiveDocumentCommand : IRequest
{
    public Guid Id { get; set; }
}

public class ArchiveDocumentCommandHandler : IRequestHandler<ArchiveDocumentCommand>
{
    private readonly IDocumentDbContext _context;

    public ArchiveDocumentCommandHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ArchiveDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await _context.Documents
            .FirstOrDefaultAsync(d => d.Id == request.Id && !d.IsDeleted, cancellationToken);

        if (document == null)
            throw new InvalidOperationException($"Document with ID {request.Id} not found.");

        document.Archive();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
