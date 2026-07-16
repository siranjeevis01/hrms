using HRMS.Services.Training.Application.Interfaces;
using HRMS.Services.Training.Domain.Entities;
using MediatR;

namespace HRMS.Services.Training.Application.Commands.AddModule;

public class AddModuleCommandHandler : IRequestHandler<AddModuleCommand, Guid>
{
    private readonly ITrainingDbContext _context;

    public AddModuleCommandHandler(ITrainingDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddModuleCommand request, CancellationToken cancellationToken)
    {
        var module = CourseModule.Create(
            request.CourseId,
            request.Title,
            request.Description,
            request.Order,
            request.Duration,
            request.TenantId);

        _context.CourseModules.Add(module);
        await _context.SaveChangesAsync(cancellationToken);

        return module.Id;
    }
}
