using HRMS.Services.ProjectTask.Application.Interfaces;
using HRMS.Services.ProjectTask.Domain.Entities;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.CreateBug;

public class CreateBugCommandHandler : IRequestHandler<CreateBugCommand, Guid>
{
    private readonly IProjectTaskDbContext _context;

    public CreateBugCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateBugCommand request, CancellationToken cancellationToken)
    {
        var bug = Bug.Create(
            request.StoryId,
            request.ProjectId,
            request.Title,
            request.Description,
            request.StepsToReproduce,
            request.ExpectedBehavior,
            request.ActualBehavior,
            request.Severity,
            request.Priority,
            request.AssignedTo,
            request.FoundBy,
            request.TenantId);

        _context.Bugs.Add(bug);
        await _context.SaveChangesAsync(cancellationToken);

        return bug.Id;
    }
}
