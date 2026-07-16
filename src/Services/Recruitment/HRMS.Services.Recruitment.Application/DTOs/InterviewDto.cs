using HRMS.Services.Recruitment.Domain.Enums;

namespace HRMS.Services.Recruitment.Application.DTOs;

public class InterviewDto
{
    public Guid Id { get; set; }
    public Guid JobApplicationId { get; set; }
    public Guid CandidateId { get; set; }
    public string? CandidateName { get; set; }
    public string InterviewerIds { get; set; } = "[]";
    public int Round { get; set; }
    public InterviewType Type { get; set; }
    public DateTime ScheduledAt { get; set; }
    public int Duration { get; set; }
    public string? Location { get; set; }
    public string? MeetingUrl { get; set; }
    public InterviewStatus Status { get; set; }
    public decimal? Rating { get; set; }
    public HireRecommendation? OverallRecommendation { get; set; }
    public Guid TenantId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
