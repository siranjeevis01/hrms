using HRMS.Services.Employee.Domain.Enums;

namespace HRMS.Services.Employee.Domain.Entities;

public class EmployeeDocument : BaseEntity
{
    public Guid EmployeeId { get; private set; }
    public DocumentType DocumentType { get; private set; }
    public string FileName { get; private set; } = string.Empty;
    public string FileUrl { get; private set; } = string.Empty;
    public long FileSize { get; private set; }
    public string MimeType { get; private set; } = string.Empty;
    public DateTime? ExpiryDate { get; private set; }
    public bool IsVerified { get; private set; }
    public Guid? VerifiedBy { get; private set; }
    public DateTime? VerifiedAt { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private EmployeeDocument() { }

    public static EmployeeDocument Create(
        Guid employeeId, DocumentType documentType, string fileName, string fileUrl,
        long fileSize, string mimeType, DateTime? expiryDate, string tenantId)
    {
        return new EmployeeDocument
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            DocumentType = documentType,
            FileName = fileName,
            FileUrl = fileUrl,
            FileSize = fileSize,
            MimeType = mimeType,
            ExpiryDate = expiryDate,
            IsVerified = false,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void UpdateDetails(DocumentType? documentType, string? fileName, string? fileUrl,
        DateTime? expiryDate)
    {
        if (documentType.HasValue) DocumentType = documentType.Value;
        FileName = fileName ?? FileName;
        FileUrl = fileUrl ?? FileUrl;
        if (expiryDate.HasValue) ExpiryDate = expiryDate;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Verify(Guid verifiedBy)
    {
        IsVerified = true;
        VerifiedBy = verifiedBy;
        VerifiedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
