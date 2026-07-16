using MediatR;

namespace HRMS.Services.Performance.Application.Commands.CompletePerformanceReview;

public class CompletePerformanceReviewCommand : IRequest
{
    public Guid ReviewId { get; set; }
    public decimal? OverallRating { get; set; }
    public decimal? OverallScore { get; set; }
}
