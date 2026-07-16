using HRMS.Services.Recruitment.Application.Interfaces;
using HRMS.Services.Recruitment.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Recruitment.Application.Commands.RejectCandidate;

public class RejectCandidateCommandHandler : IRequestHandler<RejectCandidateCommand, Unit>
{
    private readonly IRecruitmentDbContext _context;

    public RejectCandidateCommandHandler(IRecruitmentDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(RejectCandidateCommand request, CancellationToken cancellationToken)
    {
        var candidate = await _context.Candidates
            .FirstOrDefaultAsync(c => c.Id == request.CandidateId, cancellationToken)
            ?? throw new InvalidOperationException($"Candidate with ID {request.CandidateId} not found.");

        candidate.UpdateStatus(CandidateStatus.Rejected, request.Reason);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
