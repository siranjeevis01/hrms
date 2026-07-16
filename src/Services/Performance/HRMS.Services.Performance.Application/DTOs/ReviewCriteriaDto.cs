namespace HRMS.Services.Performance.Application.DTOs;

public class ReviewCriteriaDto
{
    public Guid Id { get; set; }
    public Guid PerformanceReviewId { get; set; }
    public string Category { get; set; } = string.Empty;
    public string CriteriaName { get; set; } = string.Empty;
    public decimal? Rating { get; set; }
    public decimal Weight { get; set; }
    public string? Comments { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
