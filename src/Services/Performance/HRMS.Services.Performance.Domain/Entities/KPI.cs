using HRMS.Services.Performance.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Performance.Domain.Entities;

public class KPI : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public KPICategory Category { get; private set; }
    public Guid EmployeeId { get; private set; }
    public Guid? DepartmentId { get; private set; }
    public KPIMetricType MetricType { get; private set; }
    public decimal TargetValue { get; private set; }
    public decimal CurrentValue { get; private set; }
    public string? Unit { get; private set; }
    public decimal Weight { get; private set; }
    public ScoringMethod ScoringMethod { get; private set; }
    public string Period { get; private set; } = string.Empty;
    public new string TenantId { get; private set; } = string.Empty;

    private KPI() { }

    public static KPI Create(
        string name,
        string? description,
        KPICategory category,
        Guid employeeId,
        Guid? departmentId,
        KPIMetricType metricType,
        decimal targetValue,
        string? unit,
        decimal weight,
        ScoringMethod scoringMethod,
        string period,
        string tenantId)
    {
        return new KPI
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Category = category,
            EmployeeId = employeeId,
            DepartmentId = departmentId,
            MetricType = metricType,
            TargetValue = targetValue,
            CurrentValue = 0,
            Unit = unit,
            Weight = weight,
            ScoringMethod = scoringMethod,
            Period = period,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string? name,
        string? description,
        KPICategory? category,
        decimal? targetValue,
        string? unit,
        decimal? weight,
        ScoringMethod? scoringMethod,
        string? period)
    {
        Name = name ?? Name;
        Description = description ?? Description;
        Category = category ?? Category;
        TargetValue = targetValue ?? TargetValue;
        Unit = unit ?? Unit;
        Weight = weight ?? Weight;
        ScoringMethod = scoringMethod ?? ScoringMethod;
        Period = period ?? Period;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateValue(decimal currentValue)
    {
        CurrentValue = currentValue;
        UpdatedAt = DateTime.UtcNow;
    }

    public decimal CalculateScore()
    {
        if (TargetValue == 0) return 0;
        return Math.Min(CurrentValue / TargetValue * 100, 100);
    }
}
