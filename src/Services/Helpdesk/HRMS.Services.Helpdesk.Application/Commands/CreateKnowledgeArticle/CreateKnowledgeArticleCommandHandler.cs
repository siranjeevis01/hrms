using HRMS.Services.Helpdesk.Application.Interfaces;
using HRMS.Services.Helpdesk.Domain.Entities;
using MediatR;

namespace HRMS.Services.Helpdesk.Application.Commands.CreateKnowledgeArticle;

public class CreateKnowledgeArticleCommandHandler : IRequestHandler<CreateKnowledgeArticleCommand, Guid>
{
    private readonly IHelpdeskDbContext _context;

    public CreateKnowledgeArticleCommandHandler(IHelpdeskDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateKnowledgeArticleCommand request, CancellationToken cancellationToken)
    {
        var article = KnowledgeArticle.Create(
            request.Title,
            request.Content,
            request.CategoryId,
            request.AuthorId,
            request.Tags,
            request.TenantId);

        _context.KnowledgeArticles.Add(article);
        await _context.SaveChangesAsync(cancellationToken);

        return article.Id;
    }
}
