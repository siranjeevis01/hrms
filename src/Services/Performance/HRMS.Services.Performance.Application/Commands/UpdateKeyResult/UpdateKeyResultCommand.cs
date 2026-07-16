using MediatR;

namespace HRMS.Services.Performance.Application.Commands.UpdateKeyResult;

public class UpdateKeyResultCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal? TargetValue { get; set; }
    public string? Unit { get; set; }
    public decimal? Weight { get; set; }
}
