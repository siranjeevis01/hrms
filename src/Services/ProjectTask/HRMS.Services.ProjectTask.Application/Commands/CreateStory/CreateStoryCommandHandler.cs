using HRMS.Services.ProjectTask.Application.Interfaces;
using HRMS.Services.ProjectTask.Domain.Entities;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.CreateStory;

public class CreateStoryCommandHandler : IRequestHandler<CreateStoryCommand, Guid>
{
    private readonly IProjectTaskDbContext _context;

    public CreateStoryCommandHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateStoryCommand request, CancellationToken cancellationToken)
    {
        var story = Story.Create(
            request.EpicId,
            request.ProjectId,
            request.Title,
            request.Description,
            request.AcceptanceCriteria,
            request.StoryPoints,
            request.Priority,
            request.TenantId);

        _context.Stories.Add(story);
        await _context.SaveChangesAsync(cancellationToken);

        return story.Id;
    }
}
