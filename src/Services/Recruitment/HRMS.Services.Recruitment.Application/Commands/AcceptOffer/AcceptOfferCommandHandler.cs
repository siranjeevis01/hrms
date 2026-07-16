using HRMS.Services.Recruitment.Application.Events;
using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Recruitment.Application.Commands.AcceptOffer;

public class AcceptOfferCommandHandler : IRequestHandler<AcceptOfferCommand, Unit>
{
    private readonly IRecruitmentDbContext _context;
    private readonly IMediator _mediator;

    public AcceptOfferCommandHandler(IRecruitmentDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(AcceptOfferCommand request, CancellationToken cancellationToken)
    {
        var offerLetter = await _context.OfferLetters
            .FirstOrDefaultAsync(o => o.Id == request.OfferLetterId, cancellationToken)
            ?? throw new InvalidOperationException($"Offer letter with ID {request.OfferLetterId} not found.");

        offerLetter.Accept();

        await _mediator.Publish(new OfferAcceptedEvent(offerLetter.Id, offerLetter.CandidateId), cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
