using HRMS.Services.Performance.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Performance.Domain.Entities;

public class KeyResult : BaseEntity
{
    public Guid GoalId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public decimal TargetValue { get; private set; }
    public decimal CurrentValue { get; private set; }
    public string? Unit { get; private set; }
    public decimal Weight { get; private set; }
    public GoalStatus Status { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private KeyResult() { }

    public static KeyResult Create(
        Guid goalId,
        string title,
        string? description,
        decimal targetValue,
        string? unit,
        decimal weight,
        string tenantId)
    {
        return new KeyResult
        {
            Id = Guid.NewGuid(),
            GoalId = goalId,
            Title = title,
            Description = description,
            TargetValue = targetValue,
            CurrentValue = 0,
            Unit = unit,
            Weight = weight,
            Status = GoalStatus.Draft,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string? title, string? description, decimal? targetValue, string? unit, decimal? weight)
    {
        Title = title ?? Title;
        Description = description ?? Description;
        TargetValue = targetValue ?? TargetValue;
        Unit = unit ?? Unit;
        Weight = weight ?? Weight;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateProgress(decimal currentValue)
    {
        CurrentValue = currentValue;
        if (CurrentValue >= TargetValue)
            Status = GoalStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }
}
