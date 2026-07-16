using HRMS.Services.Performance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.SubmitFeedback360;

public class SubmitFeedback360CommandHandler : IRequestHandler<SubmitFeedback360Command>
{
    private readonly IPerformanceDbContext _context;

    public SubmitFeedback360CommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task Handle(SubmitFeedback360Command request, CancellationToken cancellationToken)
    {
        var feedback = await _context.Feedback360s.FindAsync(new object[] { request.FeedbackId }, cancellationToken)
            ?? throw new Exception($"Feedback360 with ID {request.FeedbackId} not found.");

        feedback.Submit();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
