using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.ProjectTask.Application.Events;

public class SprintStartedEvent : DomainEvent
{
    public Guid SprintId { get; }
    public Guid ProjectId { get; }
    public string SprintName { get; }

    public SprintStartedEvent(Guid sprintId, Guid projectId, string sprintName)
        : base(nameof(SprintStartedEvent))
    {
        SprintId = sprintId;
        ProjectId = projectId;
        SprintName = sprintName;
    }
}
