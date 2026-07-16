using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Recruitment.Application.Commands.SendOffer;

public class SendOfferCommandHandler : IRequestHandler<SendOfferCommand, Unit>
{
    private readonly IRecruitmentDbContext _context;

    public SendOfferCommandHandler(IRecruitmentDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(SendOfferCommand request, CancellationToken cancellationToken)
    {
        var offerLetter = await _context.OfferLetters
            .FirstOrDefaultAsync(o => o.Id == request.OfferLetterId, cancellationToken)
            ?? throw new InvalidOperationException($"Offer letter with ID {request.OfferLetterId} not found.");

        offerLetter.Send(request.DocumentUrl);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
