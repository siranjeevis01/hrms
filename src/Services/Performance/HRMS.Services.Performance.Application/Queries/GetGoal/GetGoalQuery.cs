using HRMS.Services.Performance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Performance.Application.Queries.GetGoal;

public class GetGoalQuery : IRequest<GoalDto?>
{
    public Guid Id { get; set; }
}
