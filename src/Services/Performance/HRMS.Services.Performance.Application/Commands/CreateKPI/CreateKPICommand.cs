using HRMS.Services.Performance.Domain.Enums;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.CreateKPI;

public class CreateKPICommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public KPICategory Category { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid? DepartmentId { get; set; }
    public KPIMetricType MetricType { get; set; }
    public decimal TargetValue { get; set; }
    public string? Unit { get; set; }
    public decimal Weight { get; set; }
    public ScoringMethod ScoringMethod { get; set; }
    public string Period { get; set; } = string.Empty;
    public string TenantId { get; set; } = string.Empty;
}
