using MediatR;

namespace HRMS.Services.Performance.Application.Commands.AddReviewCriteria;

public class AddReviewCriteriaCommand : IRequest<Guid>
{
    public Guid PerformanceReviewId { get; set; }
    public string Category { get; set; } = string.Empty;
    public string CriteriaName { get; set; } = string.Empty;
    public decimal Weight { get; set; }
    public string? Comments { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
