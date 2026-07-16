using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.ProjectTask.Application.Events;

public class StoryCompletedEvent : DomainEvent
{
    public Guid StoryId { get; }
    public Guid EpicId { get; }
    public Guid ProjectId { get; }

    public StoryCompletedEvent(Guid storyId, Guid epicId, Guid projectId)
        : base(nameof(StoryCompletedEvent))
    {
        StoryId = storyId;
        EpicId = epicId;
        ProjectId = projectId;
    }
}
