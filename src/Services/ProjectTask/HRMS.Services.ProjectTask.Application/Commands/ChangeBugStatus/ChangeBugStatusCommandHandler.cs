using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Commands.ChangeBugStatus;

public class ChangeBugStatusCommandHandler : IRequestHandler<ChangeBugStatusCommand>
{
    private readonly IProjectTaskDbContext _context;

    public ChangeBugStatusCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ChangeBugStatusCommand request, CancellationToken cancellationToken)
    {
        var bug = await _context.Bugs
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Bug with ID {request.Id} not found.");

        bug.ChangeStatus(request.NewStatus);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
