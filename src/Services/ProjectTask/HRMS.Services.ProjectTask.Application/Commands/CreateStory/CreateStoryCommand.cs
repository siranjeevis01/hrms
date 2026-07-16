using HRMS.Services.ProjectTask.Domain.Enums;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.CreateStory;

public class CreateStoryCommand : IRequest<Guid>
{
    public Guid EpicId { get; set; }
    public Guid ProjectId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? AcceptanceCriteria { get; set; }
    public int StoryPoints { get; set; }
    public TaskPriority Priority { get; set; }
    public Guid TenantId { get; set; }
}
