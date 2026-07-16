using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Recruitment.Application.Commands.ScheduleInterview;

public class ScheduleInterviewCommandHandler : IRequestHandler<ScheduleInterviewCommand, Guid>
{
    private readonly IRecruitmentDbContext _context;

    public ScheduleInterviewCommandHandler(IRecruitmentDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(ScheduleInterviewCommand request, CancellationToken cancellationToken)
    {
        var interview = Domain.Entities.Interview.Create(
            request.JobApplicationId,
            request.CandidateId,
            request.InterviewerIds,
            request.Round,
            request.Type,
            request.ScheduledAt,
            request.Duration,
            request.Location,
            request.MeetingUrl,
            request.TenantId);

        _context.Interviews.Add(interview);
        await _context.SaveChangesAsync(cancellationToken);

        return interview.Id;
    }
}
