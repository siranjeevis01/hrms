using HRMS.Services.Performance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.CompletePerformanceReview;

public class CompletePerformanceReviewCommandHandler : IRequestHandler<CompletePerformanceReviewCommand>
{
    private readonly IPerformanceDbContext _context;

    public CompletePerformanceReviewCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CompletePerformanceReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _context.PerformanceReviews.FindAsync(new object[] { request.ReviewId }, cancellationToken)
            ?? throw new Exception($"Performance Review with ID {request.ReviewId} not found.");

        review.Complete(request.OverallRating, request.OverallScore);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
