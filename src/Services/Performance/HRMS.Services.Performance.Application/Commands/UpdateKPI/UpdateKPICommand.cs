using HRMS.Services.Performance.Domain.Enums;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.UpdateKPI;

public class UpdateKPICommand : IRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public KPICategory? Category { get; set; }
    public decimal? TargetValue { get; set; }
    public string? Unit { get; set; }
    public decimal? Weight { get; set; }
    public ScoringMethod? ScoringMethod { get; set; }
    public string? Period { get; set; }
}
