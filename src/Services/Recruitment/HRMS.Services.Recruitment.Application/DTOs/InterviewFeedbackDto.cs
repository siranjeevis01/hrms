using HRMS.Services.Recruitment.Domain.Enums;

namespace HRMS.Services.Recruitment.Application.DTOs;

public class InterviewFeedbackDto
{
    public Guid Id { get; set; }
    public Guid InterviewId { get; set; }
    public Guid InterviewerId { get; set; }
    public int TechnicalRating { get; set; }
    public int CommunicationRating { get; set; }
    public int CulturalFitRating { get; set; }
    public int ProblemSolvingRating { get; set; }
    public int OverallRating { get; set; }
    public string Strengths { get; set; } = "[]";
    public string Weaknesses { get; set; } = "[]";
    public string? Comments { get; set; }
    public HireRecommendation Recommendation { get; set; }
    public DateTime SubmittedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
