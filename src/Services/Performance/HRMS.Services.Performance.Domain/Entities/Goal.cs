using HRMS.Services.Performance.Domain.Enums;
using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Performance.Domain.Entities;

public class Goal : AggregateRoot
{
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public GoalCategory Category { get; private set; }
    public Guid EmployeeId { get; private set; }
    public Guid? ManagerId { get; private set; }
    public Guid? DepartmentId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public GoalStatus Status { get; private set; }
    public GoalPriority Priority { get; private set; }
    public decimal Weight { get; private set; }
    public decimal? TargetValue { get; private set; }
    public decimal? CurrentValue { get; private set; }
    public string? Unit { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private readonly List<KeyResult> _keyResults = new();
    public IReadOnlyCollection<KeyResult> KeyResults => _keyResults.AsReadOnly();

    private Goal() { }

    public static Goal Create(
        string title,
        string? description,
        GoalCategory category,
        Guid employeeId,
        Guid? managerId,
        Guid? departmentId,
        DateTime startDate,
        DateTime endDate,
        GoalPriority priority,
        decimal weight,
        decimal? targetValue,
        string? unit,
        string tenantId)
    {
        var goal = new Goal
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            Category = category,
            EmployeeId = employeeId,
            ManagerId = managerId,
            DepartmentId = departmentId,
            StartDate = startDate,
            EndDate = endDate,
            Status = GoalStatus.Draft,
            Priority = priority,
            Weight = weight,
            TargetValue = targetValue,
            CurrentValue = 0,
            Unit = unit,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return goal;
    }

    public void Update(
        string? title,
        string? description,
        GoalCategory? category,
        Guid? managerId,
        DateTime? startDate,
        DateTime? endDate,
        GoalPriority? priority,
        decimal? weight,
        decimal? targetValue,
        string? unit)
    {
        Title = title ?? Title;
        Description = description ?? Description;
        Category = category ?? Category;
        ManagerId = managerId ?? ManagerId;
        StartDate = startDate ?? StartDate;
        EndDate = endDate ?? EndDate;
        Priority = priority ?? Priority;
        Weight = weight ?? Weight;
        TargetValue = targetValue ?? TargetValue;
        Unit = unit ?? Unit;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Start()
    {
        Status = GoalStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        Status = GoalStatus.Completed;
        CurrentValue = TargetValue;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateProgress(decimal currentValue)
    {
        CurrentValue = currentValue;
        if (CurrentValue >= TargetValue && Status == GoalStatus.Active)
            Status = GoalStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        Status = GoalStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cascade()
    {
        if (EndDate < DateTime.UtcNow && Status == GoalStatus.Active)
            Status = GoalStatus.Overdue;
        UpdatedAt = DateTime.UtcNow;
    }

    internal void AddKeyResult(KeyResult keyResult)
    {
        _keyResults.Add(keyResult);
    }

    public new void RaiseEvent(INotification domainEvent)
    {
        base.RaiseEvent(domainEvent);
    }
}
