using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Commands.StartSprint;

public class StartSprintCommandHandler : IRequestHandler<StartSprintCommand>
{
    private readonly IProjectTaskDbContext _context;

    public StartSprintCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(StartSprintCommand request, CancellationToken cancellationToken)
    {
        var sprint = await _context.Sprints
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Sprint with ID {request.Id} not found.");

        sprint.Start();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
