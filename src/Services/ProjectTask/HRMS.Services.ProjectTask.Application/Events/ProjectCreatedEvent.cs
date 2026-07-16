using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.ProjectTask.Application.Events;

public class ProjectCreatedEvent : DomainEvent
{
    public Guid ProjectId { get; }
    public string ProjectCode { get; }
    public string ProjectName { get; }

    public ProjectCreatedEvent(Guid projectId, string projectCode, string projectName)
        : base(nameof(ProjectCreatedEvent))
    {
        ProjectId = projectId;
        ProjectCode = projectCode;
        ProjectName = projectName;
    }
}
