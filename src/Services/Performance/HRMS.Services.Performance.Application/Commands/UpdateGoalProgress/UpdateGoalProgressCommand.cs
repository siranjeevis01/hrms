using MediatR;

namespace HRMS.Services.Performance.Application.Commands.UpdateGoalProgress;

public class UpdateGoalProgressCommand : IRequest
{
    public Guid GoalId { get; set; }
    public decimal CurrentValue { get; set; }
}
