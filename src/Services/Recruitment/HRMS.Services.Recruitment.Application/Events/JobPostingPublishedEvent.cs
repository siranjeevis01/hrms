using MediatR;

namespace HRMS.Services.Recruitment.Application.Events;

public class JobPostingPublishedEvent : INotification
{
    public Guid JobPostingId { get; }
    public string Title { get; }

    public JobPostingPublishedEvent(Guid jobPostingId, string title)
    {
        JobPostingId = jobPostingId;
        Title = title;
    }
}
