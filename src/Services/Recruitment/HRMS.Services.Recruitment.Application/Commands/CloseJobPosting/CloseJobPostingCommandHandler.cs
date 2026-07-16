using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Recruitment.Application.Commands.CloseJobPosting;

public class CloseJobPostingCommandHandler : IRequestHandler<CloseJobPostingCommand, Unit>
{
    private readonly IRecruitmentDbContext _context;

    public CloseJobPostingCommandHandler(IRecruitmentDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CloseJobPostingCommand request, CancellationToken cancellationToken)
    {
        var jobPosting = await _context.JobPostings
            .FirstOrDefaultAsync(j => j.Id == request.JobPostingId, cancellationToken)
            ?? throw new InvalidOperationException($"Job posting with ID {request.JobPostingId} not found.");

        jobPosting.Close();

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
