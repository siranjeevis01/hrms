using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Commands.AssignStory;

public class AssignStoryCommandHandler : IRequestHandler<AssignStoryCommand>
{
    private readonly IProjectTaskDbContext _context;

    public AssignStoryCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AssignStoryCommand request, CancellationToken cancellationToken)
    {
        var story = await _context.Stories
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Story with ID {request.Id} not found.");

        story.AssignTo(request.EmployeeId);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
