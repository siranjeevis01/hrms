using HRMS.Shared.Kernel.Common;
using HRMS.Services.Recruitment.Domain.Enums;

namespace HRMS.Services.Recruitment.Domain.Entities;

public class Interview : AggregateRoot
{
    public Guid JobApplicationId { get; private set; }
    public Guid CandidateId { get; private set; }
    public string InterviewerIds { get; private set; } = "[]";
    public int Round { get; private set; }
    public InterviewType Type { get; private set; }
    public DateTime ScheduledAt { get; private set; }
    public int Duration { get; private set; }
    public string? Location { get; private set; }
    public string? MeetingUrl { get; private set; }
    public new InterviewStatus Status { get; private set; }
    public decimal? Rating { get; private set; }
    public HireRecommendation? OverallRecommendation { get; private set; }
    public new Guid TenantId { get; private set; }

    public JobApplication? JobApplication { get; set; }
    public Candidate? Candidate { get; set; }
    private readonly List<InterviewFeedback> _feedback = new();
    public IReadOnlyCollection<InterviewFeedback> Feedback => _feedback.AsReadOnly();

    private Interview() { }

    public static Interview Create(
        Guid jobApplicationId, Guid candidateId, string interviewerIds,
        int round, InterviewType type, DateTime scheduledAt, int duration,
        string? location, string? meetingUrl, Guid tenantId)
    {
        return new Interview
        {
            Id = Guid.NewGuid(),
            JobApplicationId = jobApplicationId,
            CandidateId = candidateId,
            InterviewerIds = interviewerIds,
            Round = round,
            Type = type,
            ScheduledAt = scheduledAt,
            Duration = duration,
            Location = location,
            MeetingUrl = meetingUrl,
            Status = InterviewStatus.Scheduled,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Reschedule(DateTime newScheduledAt, string? newLocation, string? newMeetingUrl)
    {
        ScheduledAt = newScheduledAt;
        if (newLocation != null) Location = newLocation;
        if (newMeetingUrl != null) MeetingUrl = newMeetingUrl;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete(decimal? rating, HireRecommendation? recommendation)
    {
        Status = InterviewStatus.Completed;
        Rating = rating;
        OverallRecommendation = recommendation;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        Status = InterviewStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkNoShow()
    {
        Status = InterviewStatus.NoShow;
        UpdatedAt = DateTime.UtcNow;
    }
}
