using MediatR;

namespace HRMS.Services.Performance.Application.Commands.StartGoal;

public class StartGoalCommand : IRequest
{
    public Guid GoalId { get; set; }
}
