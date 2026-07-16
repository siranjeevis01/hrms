using HRMS.Services.Document.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Document.Domain.Entities;

public class DocumentShare : BaseEntity
{
    public Guid DocumentId { get; private set; }
    public Guid SharedWithUserId { get; private set; }
    public DocumentPermission Permission { get; private set; }
    public Guid SharedBy { get; private set; }
    public DateTime SharedAt { get; private set; }
    public DateTime? ExpiresAt { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private DocumentShare() { }

    public static DocumentShare Create(
        Guid documentId,
        Guid sharedWithUserId,
        DocumentPermission permission,
        Guid sharedBy,
        DateTime? expiresAt,
        string tenantId)
    {
        return new DocumentShare
        {
            Id = Guid.NewGuid(),
            DocumentId = documentId,
            SharedWithUserId = sharedWithUserId,
            Permission = permission,
            SharedBy = sharedBy,
            SharedAt = DateTime.UtcNow,
            ExpiresAt = expiresAt,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
