using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Training.Domain.Entities;

public class Certificate : BaseEntity
{
    public Guid CourseId { get; private set; }
    public Guid EmployeeId { get; private set; }
    public string CertificateNumber { get; private set; } = string.Empty;
    public DateTime IssuedAt { get; private set; }
    public DateTime? ExpiryDate { get; private set; }
    public string? PdfUrl { get; private set; }

    private Certificate() { }

    public static Certificate Create(
        Guid courseId,
        Guid employeeId,
        string certificateNumber,
        DateTime? expiryDate,
        string? pdfUrl,
        Guid tenantId)
    {
        return new Certificate
        {
            Id = Guid.NewGuid(),
            CourseId = courseId,
            EmployeeId = employeeId,
            CertificateNumber = certificateNumber,
            IssuedAt = DateTime.UtcNow,
            ExpiryDate = expiryDate,
            PdfUrl = pdfUrl,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
