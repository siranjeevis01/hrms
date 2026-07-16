using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Document.Domain.Entities;

public class DocumentVersion : BaseEntity
{
    public Guid DocumentId { get; private set; }
    public int VersionNumber { get; private set; }
    public string FileUrl { get; private set; } = string.Empty;
    public long FileSize { get; private set; }
    public Guid UploadedBy { get; private set; }
    public string? ChangeNotes { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private DocumentVersion() { }

    public static DocumentVersion Create(
        Guid documentId,
        int versionNumber,
        string fileUrl,
        long fileSize,
        Guid uploadedBy,
        string? changeNotes,
        string tenantId)
    {
        return new DocumentVersion
        {
            Id = Guid.NewGuid(),
            DocumentId = documentId,
            VersionNumber = versionNumber,
            FileUrl = fileUrl,
            FileSize = fileSize,
            UploadedBy = uploadedBy,
            ChangeNotes = changeNotes,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
