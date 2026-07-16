using HRMS.Services.Document.Application.Interfaces;
using HRMS.Services.Document.Domain.Enums;
using MediatR;
using DocEntities = HRMS.Services.Document.Domain.Entities;

namespace HRMS.Services.Document.Application.Commands.CreateDocumentTemplate;

public class CreateDocumentTemplateCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DocumentCategory Category { get; set; }
    public string FileUrl { get; set; } = string.Empty;
    public string? Placeholders { get; set; }
    public Guid CreatedBy { get; set; }
    public bool IsPublic { get; set; }
    public string TenantId { get; set; } = string.Empty;
}

public class CreateDocumentTemplateCommandHandler : IRequestHandler<CreateDocumentTemplateCommand, Guid>
{
    private readonly IDocumentDbContext _context;

    public CreateDocumentTemplateCommandHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateDocumentTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = DocEntities.DocumentTemplate.Create(
            request.Name,
            request.Description,
            request.Category,
            request.FileUrl,
            request.Placeholders,
            request.CreatedBy,
            request.IsPublic,
            request.TenantId);

        _context.DocumentTemplates.Add(template);
        await _context.SaveChangesAsync(cancellationToken);

        return template.Id;
    }
}
