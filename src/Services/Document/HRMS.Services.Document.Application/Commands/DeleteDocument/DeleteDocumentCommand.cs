using HRMS.Services.Document.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Document.Application.Commands.DeleteDocument;

public class DeleteDocumentCommand : IRequest
{
    public Guid Id { get; set; }
}

public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand>
{
    private readonly IDocumentDbContext _context;

    public DeleteDocumentCommandHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await _context.Documents
            .FirstOrDefaultAsync(d => d.Id == request.Id && !d.IsDeleted, cancellationToken);

        if (document == null)
            throw new InvalidOperationException($"Document with ID {request.Id} not found.");

        document.Delete();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
