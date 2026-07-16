using HRMS.Services.Performance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.UpdateGoalProgress;

public class UpdateGoalProgressCommandHandler : IRequestHandler<UpdateGoalProgressCommand>
{
    private readonly IPerformanceDbContext _context;

    public UpdateGoalProgressCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateGoalProgressCommand request, CancellationToken cancellationToken)
    {
        var goal = await _context.Goals.FindAsync(new object[] { request.GoalId }, cancellationToken)
            ?? throw new Exception($"Goal with ID {request.GoalId} not found.");

        goal.UpdateProgress(request.CurrentValue);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
