using HRMS.Services.ProjectTask.Domain.Enums;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.UpdateStory;

public class UpdateStoryCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? AcceptanceCriteria { get; set; }
    public int? StoryPoints { get; set; }
    public TaskPriority? Priority { get; set; }
}
