using MediatR;

namespace HRMS.Services.Recruitment.Application.Commands.RejectOffer;

public class RejectOfferCommand : IRequest<Unit>
{
    public Guid OfferLetterId { get; set; }
    public string? Reason { get; set; }
}
