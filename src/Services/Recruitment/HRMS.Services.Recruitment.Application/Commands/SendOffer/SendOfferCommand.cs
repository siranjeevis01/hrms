using MediatR;

namespace HRMS.Services.Recruitment.Application.Commands.SendOffer;

public class SendOfferCommand : IRequest<Unit>
{
    public Guid OfferLetterId { get; set; }
    public string DocumentUrl { get; set; } = string.Empty;
}
