using HRMS.Services.Performance.Application.Interfaces;
using HRMS.Services.Performance.Domain.Entities;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.AddFeedbackAnswer;

public class AddFeedbackAnswerCommandHandler : IRequestHandler<AddFeedbackAnswerCommand, Guid>
{
    private readonly IPerformanceDbContext _context;

    public AddFeedbackAnswerCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddFeedbackAnswerCommand request, CancellationToken cancellationToken)
    {
        var answer = FeedbackAnswer.Create(
            request.Feedback360Id,
            request.Question,
            request.Rating,
            request.Comments,
            request.TenantId);

        _context.FeedbackAnswers.Add(answer);
        await _context.SaveChangesAsync(cancellationToken);

        return answer.Id;
    }
}
