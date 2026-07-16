using HRMS.Services.Document.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Document.Application.Commands.RevokeShare;

public class RevokeShareCommand : IRequest
{
    public Guid DocumentId { get; set; }
    public Guid ShareId { get; set; }
}

public class RevokeShareCommandHandler : IRequestHandler<RevokeShareCommand>
{
    private readonly IDocumentDbContext _context;

    public RevokeShareCommandHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RevokeShareCommand request, CancellationToken cancellationToken)
    {
        var document = await _context.Documents
            .FirstOrDefaultAsync(d => d.Id == request.DocumentId && !d.IsDeleted, cancellationToken);

        if (document == null)
            throw new InvalidOperationException($"Document with ID {request.DocumentId} not found.");

        document.RevokeShare(request.ShareId);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
