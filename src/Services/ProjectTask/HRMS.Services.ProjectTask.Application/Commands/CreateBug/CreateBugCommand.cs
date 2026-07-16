using HRMS.Services.ProjectTask.Domain.Enums;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.CreateBug;

public class CreateBugCommand : IRequest<Guid>
{
    public Guid? StoryId { get; set; }
    public Guid ProjectId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? StepsToReproduce { get; set; }
    public string? ExpectedBehavior { get; set; }
    public string? ActualBehavior { get; set; }
    public BugSeverity Severity { get; set; }
    public TaskPriority Priority { get; set; }
    public Guid? AssignedTo { get; set; }
    public Guid? FoundBy { get; set; }
    public Guid TenantId { get; set; }
}
