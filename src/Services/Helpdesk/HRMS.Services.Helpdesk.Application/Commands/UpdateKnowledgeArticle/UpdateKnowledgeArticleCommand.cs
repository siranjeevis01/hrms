using MediatR;

namespace HRMS.Services.Helpdesk.Application.Commands.UpdateKnowledgeArticle;

public class UpdateKnowledgeArticleCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public Guid? CategoryId { get; set; }
    public string? Tags { get; set; }
}
