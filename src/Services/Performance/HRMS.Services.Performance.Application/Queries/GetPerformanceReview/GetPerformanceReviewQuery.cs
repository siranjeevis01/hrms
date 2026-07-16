using HRMS.Services.Performance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Performance.Application.Queries.GetPerformanceReview;

public class GetPerformanceReviewQuery : IRequest<PerformanceReviewDto?>
{
    public Guid Id { get; set; }
}
