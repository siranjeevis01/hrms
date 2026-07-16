using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Training.Application.DTOs;

public class CertificateDto : BaseDto
{
    public Guid CourseId { get; set; }
    public Guid EmployeeId { get; set; }
    public string CertificateNumber { get; set; } = string.Empty;
    public DateTime IssuedAt { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? PdfUrl { get; set; }
    public string? CourseTitle { get; set; }
    public string? EmployeeName { get; set; }
}
