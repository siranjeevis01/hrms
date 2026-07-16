using HRMS.Services.Performance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.ApprovePerformanceReview;

public class ApprovePerformanceReviewCommandHandler : IRequestHandler<ApprovePerformanceReviewCommand>
{
    private readonly IPerformanceDbContext _context;

    public ApprovePerformanceReviewCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ApprovePerformanceReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _context.PerformanceReviews.FindAsync(new object[] { request.ReviewId }, cancellationToken)
            ?? throw new Exception($"Performance Review with ID {request.ReviewId} not found.");

        review.Approve();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
