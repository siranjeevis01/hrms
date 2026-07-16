using HRMS.Services.ProjectTask.Application.Interfaces;
using HRMS.Services.ProjectTask.Domain.Entities;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.CreateEpic;

public class CreateEpicCommandHandler : IRequestHandler<CreateEpicCommand, Guid>
{
    private readonly IProjectTaskDbContext _context;

    public CreateEpicCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateEpicCommand request, CancellationToken cancellationToken)
    {
        var epic = Epic.Create(
            request.ProjectId,
            request.Title,
            request.Description,
            request.Priority,
            request.StartDate,
            request.TargetDate,
            request.TenantId);

        _context.Epics.Add(epic);
        await _context.SaveChangesAsync(cancellationToken);

        return epic.Id;
    }
}
