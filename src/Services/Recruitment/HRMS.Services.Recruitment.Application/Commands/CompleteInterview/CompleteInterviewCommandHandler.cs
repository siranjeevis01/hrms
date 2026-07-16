using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Recruitment.Application.Commands.CompleteInterview;

public class CompleteInterviewCommandHandler : IRequestHandler<CompleteInterviewCommand, Unit>
{
    private readonly IRecruitmentDbContext _context;

    public CompleteInterviewCommandHandler(IRecruitmentDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CompleteInterviewCommand request, CancellationToken cancellationToken)
    {
        var interview = await _context.Interviews
            .FirstOrDefaultAsync(i => i.Id == request.InterviewId, cancellationToken)
            ?? throw new InvalidOperationException($"Interview with ID {request.InterviewId} not found.");

        interview.Complete(request.Rating, request.Recommendation);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
