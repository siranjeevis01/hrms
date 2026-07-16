using HRMS.Services.Employee.Domain.Enums;

namespace HRMS.Services.Employee.Application.DTOs;

public class EmployeeDocumentDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DocumentType DocumentType { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string MimeType { get; set; } = string.Empty;
    public DateTime? ExpiryDate { get; set; }
    public bool IsVerified { get; set; }
    public Guid? VerifiedBy { get; set; }
    public DateTime? VerifiedAt { get; set; }
}
