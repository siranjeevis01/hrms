using HRMS.Services.Recruitment.Application.Events;
using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Recruitment.Application.Commands.ApplyForJob;

public class ApplyForJobCommandHandler : IRequestHandler<ApplyForJobCommand, Guid>
{
    private readonly IRecruitmentDbContext _context;
    private readonly IMediator _mediator;

    public ApplyForJobCommandHandler(IRecruitmentDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(ApplyForJobCommand request, CancellationToken cancellationToken)
    {
        var existingApplication = await _context.JobApplications
            .FirstOrDefaultAsync(a => a.JobPostingId == request.JobPostingId && a.CandidateId == request.CandidateId, cancellationToken);

        if (existingApplication != null)
            throw new InvalidOperationException("Candidate has already applied for this job posting.");

        var application = Domain.Entities.JobApplication.Create(
            request.JobPostingId,
            request.CandidateId,
            request.TenantId);

        await _mediator.Publish(new ApplicationSubmittedEvent(application.Id, request.JobPostingId, request.CandidateId), cancellationToken);

        _context.JobApplications.Add(application);
        await _context.SaveChangesAsync(cancellationToken);

        return application.Id;
    }
}
