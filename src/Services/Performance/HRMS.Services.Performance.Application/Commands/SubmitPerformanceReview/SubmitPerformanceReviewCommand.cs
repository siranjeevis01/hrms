using MediatR;

namespace HRMS.Services.Performance.Application.Commands.SubmitPerformanceReview;

public class SubmitPerformanceReviewCommand : IRequest
{
    public Guid ReviewId { get; set; }
}
