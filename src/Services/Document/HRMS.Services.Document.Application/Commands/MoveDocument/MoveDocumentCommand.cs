using HRMS.Services.Document.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Document.Application.Commands.MoveDocument;

public class MoveDocumentCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid? FolderId { get; set; }
}

public class MoveDocumentCommandHandler : IRequestHandler<MoveDocumentCommand>
{
    private readonly IDocumentDbContext _context;

    public MoveDocumentCommandHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task Handle(MoveDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await _context.Documents
            .FirstOrDefaultAsync(d => d.Id == request.Id && !d.IsDeleted, cancellationToken);

        if (document == null)
            throw new InvalidOperationException($"Document with ID {request.Id} not found.");

        document.Move(request.FolderId);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
