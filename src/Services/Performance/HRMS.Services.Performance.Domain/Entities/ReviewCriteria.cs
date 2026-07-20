using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Performance.Domain.Entities;

public class ReviewCriteria : BaseEntity
{
    public Guid PerformanceReviewId { get; private set; }
    public string Category { get; private set; } = string.Empty;
    public string CriteriaName { get; private set; } = string.Empty;
    public decimal? Rating { get; private set; }
    public decimal Weight { get; private set; }
    public string? Comments { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private ReviewCriteria() { }

    public static ReviewCriteria Create(
        Guid performanceReviewId,
        string category,
        string criteriaName,
        decimal weight,
        string? comments,
        string tenantId)
    {
        return new ReviewCriteria
        {
            Id = Guid.NewGuid(),
            PerformanceReviewId = performanceReviewId,
            Category = category,
            CriteriaName = criteriaName,
            Weight = weight,
            Comments = comments,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Rate(decimal rating, string? comments)
    {
        Rating = rating;
        Comments = comments ?? Comments;
        UpdatedAt = DateTime.UtcNow;
    }
}
