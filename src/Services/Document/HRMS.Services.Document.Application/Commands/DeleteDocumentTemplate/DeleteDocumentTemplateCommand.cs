using HRMS.Services.Document.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Document.Application.Commands.DeleteDocumentTemplate;

public class DeleteDocumentTemplateCommand : IRequest
{
    public Guid Id { get; set; }
}

public class DeleteDocumentTemplateCommandHandler : IRequestHandler<DeleteDocumentTemplateCommand>
{
    private readonly IDocumentDbContext _context;

    public DeleteDocumentTemplateCommandHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteDocumentTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = await _context.DocumentTemplates
            .FirstOrDefaultAsync(t => t.Id == request.Id && !t.IsDeleted, cancellationToken);

        if (template == null)
            throw new InvalidOperationException($"Document template with ID {request.Id} not found.");

        template.MarkAsDeleted();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
