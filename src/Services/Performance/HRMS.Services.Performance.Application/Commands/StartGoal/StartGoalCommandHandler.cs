using HRMS.Services.Performance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.StartGoal;

public class StartGoalCommandHandler : IRequestHandler<StartGoalCommand>
{
    private readonly IPerformanceDbContext _context;

    public StartGoalCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task Handle(StartGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = await _context.Goals.FindAsync(new object[] { request.GoalId }, cancellationToken)
            ?? throw new Exception($"Goal with ID {request.GoalId} not found.");

        goal.Start();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
