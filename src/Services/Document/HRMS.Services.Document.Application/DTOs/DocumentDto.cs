namespace HRMS.Services.Document.Application.DTOs;

public class DocumentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string FileUrl { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public Guid? FolderId { get; set; }
    public string? FolderName { get; set; }
    public Guid UploadedBy { get; set; }
    public string? UploadedByName { get; set; }
    public string? Description { get; set; }
    public string? Tags { get; set; }
    public bool IsPublic { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string TenantId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int VersionCount { get; set; }
}
