using HRMS.Services.Performance.Domain.Enums;

namespace HRMS.Services.Performance.Application.DTOs;

public class PerformanceReviewDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid ReviewerId { get; set; }
    public string ReviewPeriod { get; set; } = string.Empty;
    public ReviewType ReviewType { get; set; }
    public ReviewStatus Status { get; set; }
    public decimal? OverallRating { get; set; }
    public decimal? OverallScore { get; set; }
    public string? Strengths { get; set; }
    public string? AreasForImprovement { get; set; }
    public string? Comments { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public List<ReviewCriteriaDto> Criteria { get; set; } = new();
}
