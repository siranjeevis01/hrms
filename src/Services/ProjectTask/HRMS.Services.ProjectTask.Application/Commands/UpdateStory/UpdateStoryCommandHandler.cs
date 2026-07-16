using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Commands.UpdateStory;

public class UpdateStoryCommandHandler : IRequestHandler<UpdateStoryCommand>
{
    private readonly IProjectTaskDbContext _context;

    public UpdateStoryCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateStoryCommand request, CancellationToken cancellationToken)
    {
        var story = await _context.Stories
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Story with ID {request.Id} not found.");

        story.Update(request.Title, request.Description, request.AcceptanceCriteria, request.StoryPoints, request.Priority);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
