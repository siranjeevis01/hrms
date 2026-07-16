using HRMS.Services.Performance.Domain.Enums;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.UpdateGoal;

public class UpdateGoalCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public GoalCategory? Category { get; set; }
    public Guid? ManagerId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public GoalPriority? Priority { get; set; }
    public decimal? Weight { get; set; }
    public decimal? TargetValue { get; set; }
    public string? Unit { get; set; }
}
