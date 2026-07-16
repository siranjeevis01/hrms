using HRMS.Services.Helpdesk.Application.DTOs;
using MediatR;

namespace HRMS.Services.Helpdesk.Application.Queries.GetKnowledgeArticle;

public class GetKnowledgeArticleQuery : IRequest<KnowledgeArticleDto?>
{
    public Guid Id { get; set; }
}
