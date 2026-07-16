using HRMS.Services.Performance.Application.Events;
using HRMS.Services.Performance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.CompleteGoal;

public class CompleteGoalCommandHandler : IRequestHandler<CompleteGoalCommand>
{
    private readonly IPerformanceDbContext _context;

    public CompleteGoalCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CompleteGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = await _context.Goals.FindAsync(new object[] { request.GoalId }, cancellationToken)
            ?? throw new Exception($"Goal with ID {request.GoalId} not found.");

        goal.Complete();

        goal.RaiseEvent(new GoalCompletedEvent(goal.Id, goal.EmployeeId, goal.Title));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
