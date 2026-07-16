using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Recruitment.Application.Commands.CreateOffer;

public class CreateOfferCommandHandler : IRequestHandler<CreateOfferCommand, Guid>
{
    private readonly IRecruitmentDbContext _context;

    public CreateOfferCommandHandler(IRecruitmentDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
    {
        var offerLetter = Domain.Entities.OfferLetter.Create(
            request.JobApplicationId,
            request.CandidateId,
            request.Position,
            request.DepartmentId,
            request.DesignationId,
            request.CTC,
            request.BasicSalary,
            request.JoiningDate,
            request.OfferExpiryDate,
            request.TenantId);

        _context.OfferLetters.Add(offerLetter);
        await _context.SaveChangesAsync(cancellationToken);

        return offerLetter.Id;
    }
}
