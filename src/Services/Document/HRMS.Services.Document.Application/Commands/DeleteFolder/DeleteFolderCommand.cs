using HRMS.Services.Document.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Document.Application.Commands.DeleteFolder;

public class DeleteFolderCommand : IRequest
{
    public Guid Id { get; set; }
}

public class DeleteFolderCommandHandler : IRequestHandler<DeleteFolderCommand>
{
    private readonly IDocumentDbContext _context;

    public DeleteFolderCommandHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteFolderCommand request, CancellationToken cancellationToken)
    {
        var folder = await _context.DocumentFolders
            .FirstOrDefaultAsync(f => f.Id == request.Id && !f.IsDeleted, cancellationToken);

        if (folder == null)
            throw new InvalidOperationException($"Folder with ID {request.Id} not found.");

        if (folder.IsSystem)
            throw new InvalidOperationException("System folders cannot be deleted.");

        var hasDocuments = await _context.Documents
            .AnyAsync(d => d.FolderId == request.Id && !d.IsDeleted, cancellationToken);

        if (hasDocuments)
            throw new InvalidOperationException("Cannot delete folder that contains documents.");

        folder.MarkAsDeleted();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
