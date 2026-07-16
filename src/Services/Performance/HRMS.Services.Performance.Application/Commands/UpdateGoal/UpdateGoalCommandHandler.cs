using HRMS.Services.Performance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Performance.Application.Commands.UpdateGoal;

public class UpdateGoalCommandHandler : IRequestHandler<UpdateGoalCommand>
{
    private readonly IPerformanceDbContext _context;

    public UpdateGoalCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = await _context.Goals.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new Exception($"Goal with ID {request.Id} not found.");

        goal.Update(
            request.Title,
            request.Description,
            request.Category,
            request.ManagerId,
            request.StartDate,
            request.EndDate,
            request.Priority,
            request.Weight,
            request.TargetValue,
            request.Unit);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
