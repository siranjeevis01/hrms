using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Recruitment.Application.Commands.SubmitInterviewFeedback;

public class SubmitInterviewFeedbackCommandHandler : IRequestHandler<SubmitInterviewFeedbackCommand, Guid>
{
    private readonly IRecruitmentDbContext _context;

    public SubmitInterviewFeedbackCommandHandler(IRecruitmentDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(SubmitInterviewFeedbackCommand request, CancellationToken cancellationToken)
    {
        var feedback = Domain.Entities.InterviewFeedback.Create(
            request.InterviewId,
            request.InterviewerId,
            request.TechnicalRating,
            request.CommunicationRating,
            request.CulturalFitRating,
            request.ProblemSolvingRating,
            request.OverallRating,
            request.Strengths,
            request.Weaknesses,
            request.Comments,
            request.Recommendation);

        _context.InterviewFeedbacks.Add(feedback);
        await _context.SaveChangesAsync(cancellationToken);

        return feedback.Id;
    }
}
