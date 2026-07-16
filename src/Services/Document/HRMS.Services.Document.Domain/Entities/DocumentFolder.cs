using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Document.Domain.Entities;

public class DocumentFolder : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public Guid? ParentFolderId { get; private set; }
    public string Path { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public new Guid CreatedBy { get; private set; }
    public bool IsSystem { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private readonly List<Document> _documents = new();
    public IReadOnlyCollection<Document> Documents => _documents.AsReadOnly();

    private readonly List<DocumentFolder> _subFolders = new();
    public IReadOnlyCollection<DocumentFolder> SubFolders => _subFolders.AsReadOnly();

    private DocumentFolder() { }

    public static DocumentFolder Create(
        string name,
        Guid? parentFolderId,
        string path,
        string? description,
        Guid createdBy,
        bool isSystem,
        string tenantId)
    {
        return new DocumentFolder
        {
            Id = Guid.NewGuid(),
            Name = name,
            ParentFolderId = parentFolderId,
            Path = path,
            Description = description,
            CreatedBy = createdBy,
            IsSystem = isSystem,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Rename(string name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Move(Guid? parentFolderId, string newPath)
    {
        ParentFolderId = parentFolderId;
        Path = newPath;
        UpdatedAt = DateTime.UtcNow;
    }
}
