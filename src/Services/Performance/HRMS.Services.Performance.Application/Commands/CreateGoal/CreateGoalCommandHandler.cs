using HRMS.Services.Performance.Application.Interfaces;
using HRMS.Services.Performance.Domain.Entities;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.CreateGoal;

public class CreateGoalCommandHandler : IRequestHandler<CreateGoalCommand, Guid>
{
    private readonly IPerformanceDbContext _context;

    public CreateGoalCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = Goal.Create(
            request.Title,
            request.Description,
            request.Category,
            request.EmployeeId,
            request.ManagerId,
            request.DepartmentId,
            request.StartDate,
            request.EndDate,
            request.Priority,
            request.Weight,
            request.TargetValue,
            request.Unit,
            request.TenantId);

        _context.Goals.Add(goal);
        await _context.SaveChangesAsync(cancellationToken);

        return goal.Id;
    }
}
