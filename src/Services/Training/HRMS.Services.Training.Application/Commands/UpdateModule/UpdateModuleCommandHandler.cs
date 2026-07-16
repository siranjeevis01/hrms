using HRMS.Services.Training.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Commands.UpdateModule;

public class UpdateModuleCommandHandler : IRequestHandler<UpdateModuleCommand>
{
    private readonly ITrainingDbContext _context;

    public UpdateModuleCommandHandler(ITrainingDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
    {
        var module = await _context.CourseModules
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

        if (module == null)
            throw new KeyNotFoundException($"Module with Id {request.Id} not found.");

        module.Update(request.Title, request.Description, request.Order, request.Duration);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
