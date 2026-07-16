using HRMS.Services.Helpdesk.Application.DTOs;
using MediatR;

namespace HRMS.Services.Helpdesk.Application.Queries.GetKnowledgeArticles;

public class GetKnowledgeArticlesQuery : IRequest<PagedResult<KnowledgeArticleDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? CategoryId { get; set; }
    public bool? IsPublished { get; set; }
    public string? SearchTerm { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
