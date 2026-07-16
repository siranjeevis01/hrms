using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Helpdesk.Domain.Entities;

public class TicketAttachment : BaseEntity
{
    public Guid TicketId { get; private set; }
    public string FileName { get; private set; } = string.Empty;
    public string FileUrl { get; private set; } = string.Empty;
    public long FileSize { get; private set; }
    public string ContentType { get; private set; } = string.Empty;
    public DateTime UploadedAt { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private TicketAttachment() { }

    public static TicketAttachment Create(
        Guid ticketId,
        string fileName,
        string fileUrl,
        long fileSize,
        string contentType,
        string tenantId)
    {
        return new TicketAttachment
        {
            Id = Guid.NewGuid(),
            TicketId = ticketId,
            FileName = fileName,
            FileUrl = fileUrl,
            FileSize = fileSize,
            ContentType = contentType,
            UploadedAt = DateTime.UtcNow,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
