using HRMS.Services.ProjectTask.Application.Interfaces;
using HRMS.Services.ProjectTask.Domain.Entities;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.CreateSprint;

public class CreateSprintCommandHandler : IRequestHandler<CreateSprintCommand, Guid>
{
    private readonly IProjectTaskDbContext _context;

    public CreateSprintCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateSprintCommand request, CancellationToken cancellationToken)
    {
        var sprint = Sprint.Create(
            request.ProjectId,
            request.Name,
            request.Goal,
            request.StartDate,
            request.EndDate,
            request.TenantId);

        _context.Sprints.Add(sprint);
        await _context.SaveChangesAsync(cancellationToken);

        return sprint.Id;
    }
}
