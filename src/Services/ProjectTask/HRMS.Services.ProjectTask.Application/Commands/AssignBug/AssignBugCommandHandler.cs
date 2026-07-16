using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Commands.AssignBug;

public class AssignBugCommandHandler : IRequestHandler<AssignBugCommand>
{
    private readonly IProjectTaskDbContext _context;

    public AssignBugCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AssignBugCommand request, CancellationToken cancellationToken)
    {
        var bug = await _context.Bugs
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Bug with ID {request.Id} not found.");

        bug.AssignTo(request.EmployeeId);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
