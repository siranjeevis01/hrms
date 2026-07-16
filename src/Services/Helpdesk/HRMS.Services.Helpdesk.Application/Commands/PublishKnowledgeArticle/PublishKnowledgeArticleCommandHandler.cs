using HRMS.Services.Helpdesk.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Commands.PublishKnowledgeArticle;

public class PublishKnowledgeArticleCommandHandler : IRequestHandler<PublishKnowledgeArticleCommand>
{
    private readonly IHelpdeskDbContext _context;

    public PublishKnowledgeArticleCommandHandler(IHelpdeskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(PublishKnowledgeArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _context.KnowledgeArticles
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (article == null)
            throw new InvalidOperationException($"Knowledge article with ID {request.Id} not found.");

        article.Publish();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
