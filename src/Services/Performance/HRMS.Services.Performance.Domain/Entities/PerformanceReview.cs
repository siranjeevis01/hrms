using HRMS.Services.Performance.Domain.Enums;
using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Performance.Domain.Entities;

public class PerformanceReview : AggregateRoot
{
    public Guid EmployeeId { get; private set; }
    public Guid ReviewerId { get; private set; }
    public string ReviewPeriod { get; private set; } = string.Empty;
    public ReviewType ReviewType { get; private set; }
    public new ReviewStatus Status { get; private set; }
    public decimal? OverallRating { get; private set; }
    public decimal? OverallScore { get; private set; }
    public string? Strengths { get; private set; }
    public string? AreasForImprovement { get; private set; }
    public string? Comments { get; private set; }
    public DateTime? SubmittedAt { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private readonly List<ReviewCriteria> _criteria = new();
    public IReadOnlyCollection<ReviewCriteria> Criteria => _criteria.AsReadOnly();

    private PerformanceReview() { }

    public static PerformanceReview Create(
        Guid employeeId,
        Guid reviewerId,
        string reviewPeriod,
        ReviewType reviewType,
        string? strengths,
        string? areasForImprovement,
        string? comments,
        string tenantId)
    {
        return new PerformanceReview
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            ReviewerId = reviewerId,
            ReviewPeriod = reviewPeriod,
            ReviewType = reviewType,
            Status = ReviewStatus.Draft,
            Strengths = strengths,
            AreasForImprovement = areasForImprovement,
            Comments = comments,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Submit()
    {
        Status = ReviewStatus.Submitted;
        SubmittedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete(decimal? overallRating, decimal? overallScore)
    {
        Status = ReviewStatus.Completed;
        OverallRating = overallRating ?? OverallRating;
        OverallScore = overallScore ?? OverallScore;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Approve()
    {
        Status = ReviewStatus.Approved;
        UpdatedAt = DateTime.UtcNow;
    }

    internal void AddCriteria(ReviewCriteria criteria)
    {
        _criteria.Add(criteria);
    }

    public new void RaiseEvent(INotification domainEvent)
    {
        base.RaiseEvent(domainEvent);
    }
}
