using HRMS.Services.Document.Application.Interfaces;
using HRMS.Services.Document.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Document.Application.Commands.UpdateDocumentTemplate;

public class UpdateDocumentTemplateCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DocumentCategory? Category { get; set; }
    public string? FileUrl { get; set; }
    public string? Placeholders { get; set; }
    public bool? IsPublic { get; set; }
}

public class UpdateDocumentTemplateCommandHandler : IRequestHandler<UpdateDocumentTemplateCommand>
{
    private readonly IDocumentDbContext _context;

    public UpdateDocumentTemplateCommandHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateDocumentTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = await _context.DocumentTemplates
            .FirstOrDefaultAsync(t => t.Id == request.Id && !t.IsDeleted, cancellationToken);

        if (template == null)
            throw new InvalidOperationException($"Document template with ID {request.Id} not found.");

        template.Update(
            request.Name,
            request.Description,
            request.Category,
            request.FileUrl,
            request.Placeholders,
            request.IsPublic);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
