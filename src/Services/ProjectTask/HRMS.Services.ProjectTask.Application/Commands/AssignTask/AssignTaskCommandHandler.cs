using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Commands.AssignTask;

public class AssignTaskCommandHandler : IRequestHandler<AssignTaskCommand>
{
    private readonly IProjectTaskDbContext _context;

    public AssignTaskCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AssignTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.TaskItems
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Task with ID {request.Id} not found.");

        task.AssignTo(request.EmployeeId);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
