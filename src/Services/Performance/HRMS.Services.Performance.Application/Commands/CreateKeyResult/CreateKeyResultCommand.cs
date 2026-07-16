using MediatR;

namespace HRMS.Services.Performance.Application.Commands.CreateKeyResult;

public class CreateKeyResultCommand : IRequest<Guid>
{
    public Guid GoalId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal TargetValue { get; set; }
    public string? Unit { get; set; }
    public decimal Weight { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
