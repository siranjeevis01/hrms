using HRMS.Services.Performance.Domain.Enums;

namespace HRMS.Services.Performance.Application.DTOs;

public class KPIDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public KPICategory Category { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid? DepartmentId { get; set; }
    public KPIMetricType MetricType { get; set; }
    public decimal TargetValue { get; set; }
    public decimal CurrentValue { get; set; }
    public string? Unit { get; set; }
    public decimal Weight { get; set; }
    public ScoringMethod ScoringMethod { get; set; }
    public string Period { get; set; } = string.Empty;
    public string TenantId { get; set; } = string.Empty;
    public decimal Score => TargetValue == 0 ? 0 : Math.Min(CurrentValue / TargetValue * 100, 100);
}
