namespace HRMS.Services.Helpdesk.Application.DTOs;

public class KnowledgeArticleDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public Guid AuthorId { get; set; }
    public string? Tags { get; set; }
    public int ViewCount { get; set; }
    public bool IsPublished { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
