using MediatR;

namespace HRMS.Services.Recruitment.Application.Events;

public class ApplicationSubmittedEvent : INotification
{
    public Guid JobApplicationId { get; }
    public Guid JobPostingId { get; }
    public Guid CandidateId { get; }

    public ApplicationSubmittedEvent(Guid jobApplicationId, Guid jobPostingId, Guid candidateId)
    {
        JobApplicationId = jobApplicationId;
        JobPostingId = jobPostingId;
        CandidateId = candidateId;
    }
}
