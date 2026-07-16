using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.ProjectTask.Application.Events;

public class BugReportedEvent : DomainEvent
{
    public Guid BugId { get; }
    public Guid ProjectId { get; }
    public string BugTitle { get; }

    public BugReportedEvent(Guid bugId, Guid projectId, string bugTitle)
        : base(nameof(BugReportedEvent))
    {
        BugId = bugId;
        ProjectId = projectId;
        BugTitle = bugTitle;
    }
}
