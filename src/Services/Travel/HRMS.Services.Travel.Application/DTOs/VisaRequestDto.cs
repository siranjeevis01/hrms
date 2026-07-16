using HRMS.Services.Travel.Domain.Enums;

namespace HRMS.Services.Travel.Application.DTOs;

public class VisaRequestDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string Country { get; set; } = string.Empty;
    public string VisaType { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public Guid? TravelRequestId { get; set; }
    public VisaStatus Status { get; set; }
    public DateTime SubmissionDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? DocumentUrl { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
