using HRMS.Services.Performance.Domain.Enums;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.CreatePerformanceReview;

public class CreatePerformanceReviewCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public Guid ReviewerId { get; set; }
    public string ReviewPeriod { get; set; } = string.Empty;
    public ReviewType ReviewType { get; set; }
    public string? Strengths { get; set; }
    public string? AreasForImprovement { get; set; }
    public string? Comments { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
