using MediatR;

namespace HRMS.Services.Recruitment.Application.Commands.AcceptOffer;

public class AcceptOfferCommand : IRequest<Unit>
{
    public Guid OfferLetterId { get; set; }
}
