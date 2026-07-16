using HRMS.Services.Recruitment.Application.Events;
using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Recruitment.Application.Commands.CreateCandidate;

public class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, Guid>
{
    private readonly IRecruitmentDbContext _context;
    private readonly IMediator _mediator;

    public CreateCandidateCommandHandler(IRecruitmentDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
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
            request.Source,
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
