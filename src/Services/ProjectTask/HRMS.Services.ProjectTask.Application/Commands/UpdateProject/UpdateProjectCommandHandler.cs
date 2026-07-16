using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Commands.UpdateProject;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
{
    private readonly IProjectTaskDbContext _context;

    public UpdateProjectCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Project with ID {request.Id} not found.");

        project.Update(
            request.Name,
            request.Description,
            request.ClientName,
            request.StartDate,
            request.EndDate,
            request.Priority,
            request.Budget,
            request.Currency);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
