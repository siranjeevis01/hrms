using HRMS.Shared.Kernel.Common;
using HRMS.Services.Recruitment.Domain.Enums;

namespace HRMS.Services.Recruitment.Domain.Entities;

public class InterviewFeedback : BaseEntity
{
    public Guid InterviewId { get; private set; }
    public Guid InterviewerId { get; private set; }
    public int TechnicalRating { get; private set; }
    public int CommunicationRating { get; private set; }
    public int CulturalFitRating { get; private set; }
    public int ProblemSolvingRating { get; private set; }
    public int OverallRating { get; private set; }
    public string Strengths { get; private set; } = "[]";
    public string Weaknesses { get; private set; } = "[]";
    public string? Comments { get; private set; }
    public HireRecommendation Recommendation { get; private set; }
    public DateTime SubmittedAt { get; private set; }

    public Interview? Interview { get; set; }

    private InterviewFeedback() { }

    public static InterviewFeedback Create(
        Guid interviewId, Guid interviewerId,
        int technicalRating, int communicationRating,
        int culturalFitRating, int problemSolvingRating,
        int overallRating, string strengths, string weaknesses,
        string? comments, HireRecommendation recommendation)
    {
        return new InterviewFeedback
        {
            Id = Guid.NewGuid(),
            InterviewId = interviewId,
            InterviewerId = interviewerId,
            TechnicalRating = technicalRating,
            CommunicationRating = communicationRating,
            CulturalFitRating = culturalFitRating,
            ProblemSolvingRating = problemSolvingRating,
            OverallRating = overallRating,
            Strengths = strengths,
            Weaknesses = weaknesses,
            Comments = comments,
            Recommendation = recommendation,
            SubmittedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
    }
}
