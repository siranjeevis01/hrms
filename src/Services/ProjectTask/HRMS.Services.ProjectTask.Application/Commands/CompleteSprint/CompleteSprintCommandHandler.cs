using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Commands.CompleteSprint;

public class CompleteSprintCommandHandler : IRequestHandler<CompleteSprintCommand>
{
    private readonly IProjectTaskDbContext _context;

    public CompleteSprintCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CompleteSprintCommand request, CancellationToken cancellationToken)
    {
        var sprint = await _context.Sprints
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Sprint with ID {request.Id} not found.");

        sprint.Complete();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
