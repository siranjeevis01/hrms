using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Commands.ChangeProjectStatus;

public class ChangeProjectStatusCommandHandler : IRequestHandler<ChangeProjectStatusCommand>
{
    private readonly IProjectTaskDbContext _context;

    public ChangeProjectStatusCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ChangeProjectStatusCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Project with ID {request.Id} not found.");

        project.ChangeStatus(request.NewStatus);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
