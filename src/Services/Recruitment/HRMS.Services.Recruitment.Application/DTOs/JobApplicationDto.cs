using HRMS.Services.Recruitment.Domain.Enums;

namespace HRMS.Services.Recruitment.Application.DTOs;

public class JobApplicationDto
{
    public Guid Id { get; set; }
    public Guid JobPostingId { get; set; }
    public Guid CandidateId { get; set; }
    public string? CandidateName { get; set; }
    public string? JobTitle { get; set; }
    public DateTime AppliedAt { get; set; }
    public ApplicationStatus Status { get; set; }
    public decimal? ScreeningScore { get; set; }
    public string? RecruiterNotes { get; set; }
    public Guid? AssignedTo { get; set; }
    public string? RejectionReason { get; set; }
    public Guid TenantId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
