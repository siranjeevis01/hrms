using MediatR;

namespace HRMS.Services.Performance.Application.Commands.ApprovePerformanceReview;

public class ApprovePerformanceReviewCommand : IRequest
{
    public Guid ReviewId { get; set; }
}
