using HRMS.Services.Performance.Application.Interfaces;
using HRMS.Services.Performance.Domain.Entities;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.CreatePerformanceReview;

public class CreatePerformanceReviewCommandHandler : IRequestHandler<CreatePerformanceReviewCommand, Guid>
{
    private readonly IPerformanceDbContext _context;

    public CreatePerformanceReviewCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreatePerformanceReviewCommand request, CancellationToken cancellationToken)
    {
        var review = PerformanceReview.Create(
            request.EmployeeId,
            request.ReviewerId,
            request.ReviewPeriod,
            request.ReviewType,
            request.Strengths,
            request.AreasForImprovement,
            request.Comments,
            request.TenantId);

        _context.PerformanceReviews.Add(review);
        await _context.SaveChangesAsync(cancellationToken);

        return review.Id;
    }
}
