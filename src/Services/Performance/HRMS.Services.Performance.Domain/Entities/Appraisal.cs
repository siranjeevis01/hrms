using HRMS.Services.Performance.Domain.Enums;
using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Performance.Domain.Entities;

public class Appraisal : AggregateRoot
{
    public Guid EmployeeId { get; private set; }
    public Guid? ManagerId { get; private set; }
    public string Period { get; private set; } = string.Empty;
    public AppraisalType Type { get; private set; }
    public new AppraisalStatus Status { get; private set; }
    public decimal? FinalRating { get; private set; }
    public decimal? HikePercentage { get; private set; }
    public bool PromotionRecommended { get; private set; }
    public decimal? Bonus { get; private set; }
    public string? Comments { get; private set; }
    public decimal? SelfRating { get; private set; }
    public string? Achievements { get; private set; }
    public string? Goals { get; private set; }
    public string? Strengths { get; private set; }
    public string? Improvements { get; private set; }
    public Guid? ApprovedBy { get; private set; }
    public DateTime? ApprovedAt { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private Appraisal() { }

    public static Appraisal Create(
        Guid employeeId,
        Guid? managerId,
        string period,
        AppraisalType type,
        string? comments,
        string tenantId)
    {
        return new Appraisal
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            ManagerId = managerId,
            Period = period,
            Type = type,
            Status = AppraisalStatus.Draft,
            Comments = comments,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Submit()
    {
        Status = AppraisalStatus.Submitted;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SubmitSelfAssessment(decimal? selfRating, string? achievements, string? goals)
    {
        SelfRating = selfRating;
        Achievements = achievements;
        Goals = goals;
        Status = AppraisalStatus.Submitted;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Approve(
        Guid approvedBy,
        decimal? finalRating,
        decimal? hikePercentage,
        bool promotionRecommended,
        decimal? bonus)
    {
        Status = AppraisalStatus.Approved;
        ApprovedBy = approvedBy;
        ApprovedAt = DateTime.UtcNow;
        FinalRating = finalRating ?? FinalRating;
        HikePercentage = hikePercentage ?? HikePercentage;
        PromotionRecommended = promotionRecommended;
        Bonus = bonus ?? Bonus;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reject()
    {
        Status = AppraisalStatus.Rejected;
        UpdatedAt = DateTime.UtcNow;
    }

    public new void RaiseEvent(INotification domainEvent)
    {
        base.RaiseEvent(domainEvent);
    }
}
