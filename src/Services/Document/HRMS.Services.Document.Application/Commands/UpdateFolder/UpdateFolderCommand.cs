using HRMS.Services.Document.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Document.Application.Commands.UpdateFolder;

public class UpdateFolderCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class UpdateFolderCommandHandler : IRequestHandler<UpdateFolderCommand>
{
    private readonly IDocumentDbContext _context;

    public UpdateFolderCommandHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateFolderCommand request, CancellationToken cancellationToken)
    {
        var folder = await _context.DocumentFolders
            .FirstOrDefaultAsync(f => f.Id == request.Id && !f.IsDeleted, cancellationToken);

        if (folder == null)
            throw new InvalidOperationException($"Folder with ID {request.Id} not found.");

        if (folder.IsSystem)
            throw new InvalidOperationException("System folders cannot be modified.");

        if (request.Name != null)
            folder.Rename(request.Name);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
