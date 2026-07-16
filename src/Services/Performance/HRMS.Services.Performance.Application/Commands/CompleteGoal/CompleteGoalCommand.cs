using MediatR;

namespace HRMS.Services.Performance.Application.Commands.CompleteGoal;

public class CompleteGoalCommand : IRequest
{
    public Guid GoalId { get; set; }
}
