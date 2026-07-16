using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Commands.ChangeTaskStatus;

public class ChangeTaskStatusCommandHandler : IRequestHandler<ChangeTaskStatusCommand>
{
    private readonly IProjectTaskDbContext _context;

    public ChangeTaskStatusCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ChangeTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.TaskItems
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Task with ID {request.Id} not found.");

        task.ChangeStatus(request.NewStatus);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
