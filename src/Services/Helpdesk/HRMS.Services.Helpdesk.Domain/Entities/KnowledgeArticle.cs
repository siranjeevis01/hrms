using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Helpdesk.Domain.Entities;

public class KnowledgeArticle : AggregateRoot
{
    public string Title { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public Guid CategoryId { get; private set; }
    public Guid AuthorId { get; private set; }
    public string? Tags { get; private set; }
    public int ViewCount { get; private set; }
    public bool IsPublished { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private KnowledgeArticle() { }

    public static KnowledgeArticle Create(
        string title,
        string content,
        Guid categoryId,
        Guid authorId,
        string? tags,
        string tenantId)
    {
        return new KnowledgeArticle
        {
            Id = Guid.NewGuid(),
            Title = title,
            Content = content,
            CategoryId = categoryId,
            AuthorId = authorId,
            Tags = tags,
            ViewCount = 0,
            IsPublished = false,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Publish()
    {
        IsPublished = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Unpublish()
    {
        IsPublished = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncrementViewCount()
    {
        ViewCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(
        string? title,
        string? content,
        Guid? categoryId,
        string? tags)
    {
        Title = title ?? Title;
        Content = content ?? Content;
        CategoryId = categoryId ?? CategoryId;
        Tags = tags ?? Tags;
        UpdatedAt = DateTime.UtcNow;
    }
}
