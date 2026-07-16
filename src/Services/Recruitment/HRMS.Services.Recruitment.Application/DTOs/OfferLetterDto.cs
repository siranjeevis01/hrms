using HRMS.Services.Recruitment.Domain.Enums;

namespace HRMS.Services.Recruitment.Application.DTOs;

public class OfferLetterDto
{
    public Guid Id { get; set; }
    public Guid JobApplicationId { get; set; }
    public Guid CandidateId { get; set; }
    public string? CandidateName { get; set; }
    public Guid? EmployeeId { get; set; }
    public string Position { get; set; } = string.Empty;
    public Guid DepartmentId { get; set; }
    public Guid DesignationId { get; set; }
    public decimal CTC { get; set; }
    public decimal BasicSalary { get; set; }
    public DateTime JoiningDate { get; set; }
    public DateTime OfferExpiryDate { get; set; }
    public OfferStatus Status { get; set; }
    public DateTime? SentAt { get; set; }
    public DateTime? AcceptedAt { get; set; }
    public DateTime? RejectedAt { get; set; }
    public string? RejectionReason { get; set; }
    public string? DocumentUrl { get; set; }
    public Guid TenantId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
