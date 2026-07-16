using MediatR;

namespace HRMS.Services.Recruitment.Application.Events;

public class OfferAcceptedEvent : INotification
{
    public Guid OfferLetterId { get; }
    public Guid CandidateId { get; }

    public OfferAcceptedEvent(Guid offerLetterId, Guid candidateId)
    {
        OfferLetterId = offerLetterId;
        CandidateId = candidateId;
    }
}
