using MediatR;

namespace HRMS.Services.Helpdesk.Application.Commands.CreateKnowledgeArticle;

public class CreateKnowledgeArticleCommand : IRequest<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public Guid AuthorId { get; set; }
    public string? Tags { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
