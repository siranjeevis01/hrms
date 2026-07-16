using HRMS.Services.Document.Application.Interfaces;
using HRMS.Services.Document.Domain.Enums;
using MediatR;
using DocEntities = HRMS.Services.Document.Domain.Entities;

namespace HRMS.Services.Document.Application.Commands.CreateFolder;

public class CreateFolderCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public Guid? ParentFolderId { get; set; }
    public string? Description { get; set; }
    public Guid CreatedBy { get; set; }
    public string TenantId { get; set; } = string.Empty;
}

public class CreateFolderCommandHandler : IRequestHandler<CreateFolderCommand, Guid>
{
    private readonly IDocumentDbContext _context;

    public CreateFolderCommandHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
    {
        var path = request.ParentFolderId.HasValue
            ? $"/{request.ParentFolderId}/{request.Name}"
            : $"/{request.Name}";

        var folder = DocEntities.DocumentFolder.Create(
            request.Name,
            request.ParentFolderId,
            path,
            request.Description,
            request.CreatedBy,
            false,
            request.TenantId);

        _context.DocumentFolders.Add(folder);
        await _context.SaveChangesAsync(cancellationToken);

        return folder.Id;
    }
}
