using HRMS.Services.Performance.Application.Events;
using HRMS.Services.Performance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.SubmitPerformanceReview;

public class SubmitPerformanceReviewCommandHandler : IRequestHandler<SubmitPerformanceReviewCommand>
{
    private readonly IPerformanceDbContext _context;

    public SubmitPerformanceReviewCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task Handle(SubmitPerformanceReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _context.PerformanceReviews.FindAsync(new object[] { request.ReviewId }, cancellationToken)
            ?? throw new Exception($"Performance Review with ID {request.ReviewId} not found.");

        review.Submit();
        review.RaiseEvent(new ReviewSubmittedEvent(review.Id, review.EmployeeId, review.ReviewerId));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
