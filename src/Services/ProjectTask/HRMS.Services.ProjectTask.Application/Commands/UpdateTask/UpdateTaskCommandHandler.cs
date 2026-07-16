using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Commands.UpdateTask;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
{
    private readonly IProjectTaskDbContext _context;

    public UpdateTaskCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.TaskItems
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Task with ID {request.Id} not found.");

        task.Update(request.Title, request.Description, request.Priority, request.EstimatedHours, request.DueDate, request.ParentTaskId);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
