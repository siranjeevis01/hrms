using HRMS.Services.Helpdesk.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Commands.UpdateKnowledgeArticle;

public class UpdateKnowledgeArticleCommandHandler : IRequestHandler<UpdateKnowledgeArticleCommand>
{
    private readonly IHelpdeskDbContext _context;

    public UpdateKnowledgeArticleCommandHandler(IHelpdeskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateKnowledgeArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _context.KnowledgeArticles
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (article == null)
            throw new InvalidOperationException($"Knowledge article with ID {request.Id} not found.");

        article.Update(request.Title, request.Content, request.CategoryId, request.Tags);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
