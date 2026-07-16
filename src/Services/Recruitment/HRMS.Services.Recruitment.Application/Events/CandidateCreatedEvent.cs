using MediatR;

namespace HRMS.Services.Recruitment.Application.Events;

public class CandidateCreatedEvent : INotification
{
    public Guid CandidateId { get; }
    public string Email { get; }

    public CandidateCreatedEvent(Guid candidateId, string email)
    {
        CandidateId = candidateId;
        Email = email;
    }
}
