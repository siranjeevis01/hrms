using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Commands.UpdateBug;

public class UpdateBugCommandHandler : IRequestHandler<UpdateBugCommand>
{
    private readonly IProjectTaskDbContext _context;

    public UpdateBugCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateBugCommand request, CancellationToken cancellationToken)
    {
        var bug = await _context.Bugs
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Bug with ID {request.Id} not found.");

        bug.Update(request.Title, request.Description, request.StepsToReproduce, request.ExpectedBehavior, request.ActualBehavior, request.Severity, request.Priority);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
