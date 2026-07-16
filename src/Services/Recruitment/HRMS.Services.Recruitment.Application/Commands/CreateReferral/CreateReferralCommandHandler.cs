using HRMS.Services.Recruitment.Application.Events;
using HRMS.Services.Recruitment.Application.Interfaces;
using HRMS.Services.Recruitment.Domain.Enums;
using MediatR;

namespace HRMS.Services.Recruitment.Application.Commands.CreateReferral;

public class CreateReferralCommandHandler : IRequestHandler<CreateReferralCommand, Guid>
{
    private readonly IRecruitmentDbContext _context;
    private readonly IMediator _mediator;

    public CreateReferralCommandHandler(IRecruitmentDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(CreateReferralCommand request, CancellationToken cancellationToken)
    {
        var candidate = Domain.Entities.Candidate.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.CurrentCompany,
            request.CurrentDesignation,
            request.TotalExperience,
            request.ExpectedSalary,
            request.Currency,
            request.ResumeUrl,
            request.CoverLetter,
            CandidateSource.Referral,
            request.ReferralEmployeeId,
            request.Skills,
            request.Education,
            request.TenantId);

        await _mediator.Publish(new CandidateCreatedEvent(candidate.Id, candidate.Email), cancellationToken);

        _context.Candidates.Add(candidate);
        await _context.SaveChangesAsync(cancellationToken);

        return candidate.Id;
    }
}
