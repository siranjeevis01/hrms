using HRMS.Services.Document.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Document.Domain.Entities;

public class DocumentAccessLog : BaseEntity
{
    public Guid DocumentId { get; private set; }
    public Guid EmployeeId { get; private set; }
    public DateTime AccessedAt { get; private set; }
    public DocumentAccessAction Action { get; private set; }
    public string? IpAddress { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private DocumentAccessLog() { }

    public static DocumentAccessLog Create(
        Guid documentId,
        Guid employeeId,
        DocumentAccessAction action,
        string? ipAddress,
        string tenantId)
    {
        return new DocumentAccessLog
        {
            Id = Guid.NewGuid(),
            DocumentId = documentId,
            EmployeeId = employeeId,
            AccessedAt = DateTime.UtcNow,
            Action = action,
            IpAddress = ipAddress,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
