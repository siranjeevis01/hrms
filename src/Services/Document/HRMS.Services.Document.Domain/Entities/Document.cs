using HRMS.Services.Document.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Document.Domain.Entities;

public class Document : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string FileName { get; private set; } = string.Empty;
    public string ContentType { get; private set; } = string.Empty;
    public long FileSize { get; private set; }
    public string FileUrl { get; private set; } = string.Empty;
    public string? ThumbnailUrl { get; private set; }
    public Guid? FolderId { get; private set; }
    public Guid UploadedBy { get; private set; }
    public string? Description { get; private set; }
    public string? Tags { get; private set; }
    public bool IsPublic { get; private set; }
    public new DocumentStatus Status { get; private set; }
    public DocumentCategory Category { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private readonly List<DocumentVersion> _versions = new();
    public IReadOnlyCollection<DocumentVersion> Versions => _versions.AsReadOnly();

    private readonly List<DocumentShare> _shares = new();
    public IReadOnlyCollection<DocumentShare> Shares => _shares.AsReadOnly();

    private readonly List<DocumentAccessLog> _accessLogs = new();
    public IReadOnlyCollection<DocumentAccessLog> AccessLogs => _accessLogs.AsReadOnly();

    private Document() { }

    public static Document Create(
        string name,
        string fileName,
        string contentType,
        long fileSize,
        string fileUrl,
        string? thumbnailUrl,
        Guid? folderId,
        Guid uploadedBy,
        string? description,
        string? tags,
        bool isPublic,
        DocumentCategory category,
        string tenantId)
    {
        var document = new Document
        {
            Id = Guid.NewGuid(),
            Name = name,
            FileName = fileName,
            ContentType = contentType,
            FileSize = fileSize,
            FileUrl = fileUrl,
            ThumbnailUrl = thumbnailUrl,
            FolderId = folderId,
            UploadedBy = uploadedBy,
            Description = description,
            Tags = tags,
            IsPublic = isPublic,
            Category = category,
            Status = DocumentStatus.Active,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        document._versions.Add(DocumentVersion.Create(
            document.Id, 1, fileUrl, fileSize, uploadedBy, "Initial upload", tenantId));

        document.RaiseEvent(new DocumentUploadedEvent(document.Id, name, uploadedBy, tenantId));

        return document;
    }

    public void Update(
        string? name,
        string? description,
        string? tags,
        bool? isPublic,
        DocumentCategory? category)
    {
        Name = name ?? Name;
        Description = description ?? Description;
        Tags = tags ?? Tags;
        if (isPublic.HasValue) IsPublic = isPublic.Value;
        if (category.HasValue) Category = category.Value;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Move(Guid? folderId)
    {
        FolderId = folderId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Share(Guid sharedWithUserId, DocumentPermission permission, Guid sharedBy, DateTime? expiresAt)
    {
        _shares.Add(DocumentShare.Create(
            Id, sharedWithUserId, permission, sharedBy, expiresAt, TenantId));
        UpdatedAt = DateTime.UtcNow;

        RaiseEvent(new DocumentSharedEvent(Id, sharedBy, sharedWithUserId, permission, TenantId));
    }

    public void RevokeShare(Guid shareId)
    {
        var share = _shares.FirstOrDefault(s => s.Id == shareId);
        if (share != null)
        {
            share.MarkAsDeleted();
        }
        UpdatedAt = DateTime.UtcNow;
    }

    public void Archive()
    {
        Status = DocumentStatus.Archived;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        Status = DocumentStatus.Deleted;
        UpdatedAt = DateTime.UtcNow;

        RaiseEvent(new DocumentDeletedEvent(Id, UploadedBy, TenantId));
    }

    public void AddVersion(string fileUrl, long fileSize, Guid uploadedBy, string? changeNotes)
    {
        var versionNumber = _versions.Count + 1;
        _versions.Add(DocumentVersion.Create(
            Id, versionNumber, fileUrl, fileSize, uploadedBy, changeNotes, TenantId));
        UpdatedAt = DateTime.UtcNow;
    }
}
