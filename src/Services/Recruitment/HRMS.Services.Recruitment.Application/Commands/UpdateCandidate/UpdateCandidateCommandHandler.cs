using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Recruitment.Application.Commands.UpdateCandidate;

public class UpdateCandidateCommandHandler : IRequestHandler<UpdateCandidateCommand, Unit>
{
    private readonly IRecruitmentDbContext _context;

    public UpdateCandidateCommandHandler(IRecruitmentDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
    {
        var candidate = await _context.Candidates
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Candidate with ID {request.Id} not found.");

        candidate.Update(
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
            request.Skills,
            request.Education,
            request.Notes);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
