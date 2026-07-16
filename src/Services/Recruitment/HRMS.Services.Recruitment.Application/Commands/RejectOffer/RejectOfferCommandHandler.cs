using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Recruitment.Application.Commands.RejectOffer;

public class RejectOfferCommandHandler : IRequestHandler<RejectOfferCommand, Unit>
{
    private readonly IRecruitmentDbContext _context;

    public RejectOfferCommandHandler(IRecruitmentDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(RejectOfferCommand request, CancellationToken cancellationToken)
    {
        var offerLetter = await _context.OfferLetters
            .FirstOrDefaultAsync(o => o.Id == request.OfferLetterId, cancellationToken)
            ?? throw new InvalidOperationException($"Offer letter with ID {request.OfferLetterId} not found.");

        offerLetter.Reject(request.Reason);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
