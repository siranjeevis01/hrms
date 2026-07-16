using HRMS.Services.ProjectTask.Application.Events;
using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Guid>
{
    private readonly IProjectTaskDbContext _context;

    public CreateProjectCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = Domain.Entities.Project.Create(
            request.Name,
            request.Description,
            request.Code,
            request.DepartmentId,
            request.ClientName,
            request.StartDate,
            request.EndDate,
            request.Priority,
            request.Budget,
            request.Currency,
            request.OwnerId,
            request.TenantId);

        _context.Projects.Add(project);
        await _context.SaveChangesAsync(cancellationToken);

        return project.Id;
    }
}
