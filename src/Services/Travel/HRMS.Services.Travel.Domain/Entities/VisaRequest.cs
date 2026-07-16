using HRMS.Services.Travel.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Travel.Domain.Entities;

public class VisaRequest : AggregateRoot
{
    public Guid EmployeeId { get; private set; }
    public string Country { get; private set; } = string.Empty;
    public string VisaType { get; private set; } = string.Empty;
    public string Purpose { get; private set; } = string.Empty;
    public Guid? TravelRequestId { get; private set; }
    public VisaStatus Status { get; private set; }
    public DateTime SubmissionDate { get; private set; }
    public DateTime? ExpiryDate { get; private set; }
    public string? DocumentUrl { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private VisaRequest() { }

    public static VisaRequest Create(
        Guid employeeId,
        string country,
        string visaType,
        string purpose,
        Guid? travelRequestId,
        DateTime submissionDate,
        string? documentUrl,
        string tenantId)
    {
        return new VisaRequest
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            Country = country,
            VisaType = visaType,
            Purpose = purpose,
            TravelRequestId = travelRequestId,
            Status = VisaStatus.Draft,
            SubmissionDate = submissionDate,
            DocumentUrl = documentUrl,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string? country,
        string? visaType,
        string? purpose,
        Guid? travelRequestId,
        DateTime? submissionDate,
        DateTime? expiryDate,
        string? documentUrl,
        VisaStatus? status)
    {
        Country = country ?? Country;
        VisaType = visaType ?? VisaType;
        Purpose = purpose ?? Purpose;
        TravelRequestId = travelRequestId ?? TravelRequestId;
        SubmissionDate = submissionDate ?? SubmissionDate;
        ExpiryDate = expiryDate ?? ExpiryDate;
        DocumentUrl = documentUrl ?? DocumentUrl;
        Status = status ?? Status;
        UpdatedAt = DateTime.UtcNow;
    }
}
