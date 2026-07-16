using MediatR;

namespace HRMS.Services.Helpdesk.Application.Commands.PublishKnowledgeArticle;

public class PublishKnowledgeArticleCommand : IRequest
{
    public Guid Id { get; set; }
}
