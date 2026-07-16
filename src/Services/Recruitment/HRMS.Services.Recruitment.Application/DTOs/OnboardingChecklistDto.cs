using HRMS.Services.Recruitment.Domain.Entities;

namespace HRMS.Services.Recruitment.Application.DTOs;

public class OnboardingChecklistDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid CandidateId { get; set; }
    public DateTime JoiningDate { get; set; }
    public string Items { get; set; } = "[]";
    public OnboardingStatus Status { get; set; }
    public DateTime? CompletedAt { get; set; }
    public Guid TenantId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
