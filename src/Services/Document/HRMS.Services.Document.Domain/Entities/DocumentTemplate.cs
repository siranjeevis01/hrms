using HRMS.Services.Document.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Document.Domain.Entities;

public class DocumentTemplate : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DocumentCategory Category { get; private set; }
    public string FileUrl { get; private set; } = string.Empty;
    public string? Placeholders { get; private set; }
    public new Guid CreatedBy { get; private set; }
    public bool IsPublic { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private DocumentTemplate() { }

    public static DocumentTemplate Create(
        string name,
        string? description,
        DocumentCategory category,
        string fileUrl,
        string? placeholders,
        Guid createdBy,
        bool isPublic,
        string tenantId)
    {
        return new DocumentTemplate
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Category = category,
            FileUrl = fileUrl,
            Placeholders = placeholders,
            CreatedBy = createdBy,
            IsPublic = isPublic,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string? name,
        string? description,
        DocumentCategory? category,
        string? fileUrl,
        string? placeholders,
        bool? isPublic)
    {
        Name = name ?? Name;
        Description = description ?? Description;
        if (category.HasValue) Category = category.Value;
        FileUrl = fileUrl ?? FileUrl;
        Placeholders = placeholders ?? Placeholders;
        if (isPublic.HasValue) IsPublic = isPublic.Value;
        UpdatedAt = DateTime.UtcNow;
    }
}
