using HRMS.Services.ProjectTask.Application.Interfaces;
using HRMS.Services.ProjectTask.Domain.Entities;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.CreateTask;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly IProjectTaskDbContext _context;

    public CreateTaskCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = Domain.Entities.TaskItem.Create(
            request.StoryId,
            request.ProjectId,
            request.Title,
            request.Description,
            request.Priority,
            request.AssignedTo,
            request.EstimatedHours,
            request.DueDate,
            request.ParentTaskId,
            request.TenantId);

        _context.TaskItems.Add(task);
        await _context.SaveChangesAsync(cancellationToken);

        return task.Id;
    }
}
