using HRMS.Services.Recruitment.Application.Events;
using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Recruitment.Application.Commands.PublishJobPosting;

public class PublishJobPostingCommandHandler : IRequestHandler<PublishJobPostingCommand, Unit>
{
    private readonly IRecruitmentDbContext _context;
    private readonly IMediator _mediator;

    public PublishJobPostingCommandHandler(IRecruitmentDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(PublishJobPostingCommand request, CancellationToken cancellationToken)
    {
        var jobPosting = await _context.JobPostings
            .FirstOrDefaultAsync(j => j.Id == request.JobPostingId, cancellationToken)
            ?? throw new InvalidOperationException($"Job posting with ID {request.JobPostingId} not found.");

        jobPosting.Publish();

        await _mediator.Publish(new JobPostingPublishedEvent(jobPosting.Id, jobPosting.Title), cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
