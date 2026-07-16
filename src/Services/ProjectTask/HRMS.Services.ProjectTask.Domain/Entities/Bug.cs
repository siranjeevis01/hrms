using HRMS.Services.ProjectTask.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.ProjectTask.Domain.Entities;

public class Bug : BaseEntity
{
    public Guid? StoryId { get; private set; }
    public Guid ProjectId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string? StepsToReproduce { get; private set; }
    public string? ExpectedBehavior { get; private set; }
    public string? ActualBehavior { get; private set; }
    public BugSeverity Severity { get; private set; }
    public new BugStatus Status { get; private set; }
    public TaskPriority Priority { get; private set; }
    public Guid? AssignedTo { get; private set; }
    public Guid? FoundBy { get; private set; }

    private Bug() { }

    public static Bug Create(
        Guid? storyId,
        Guid projectId,
        string title,
        string? description,
        string? stepsToReproduce,
        string? expectedBehavior,
        string? actualBehavior,
        BugSeverity severity,
        TaskPriority priority,
        Guid? assignedTo,
        Guid? foundBy,
        Guid tenantId)
    {
        return new Bug
        {
            Id = Guid.NewGuid(),
            StoryId = storyId,
            ProjectId = projectId,
            Title = title,
            Description = description,
            StepsToReproduce = stepsToReproduce,
            ExpectedBehavior = expectedBehavior,
            ActualBehavior = actualBehavior,
            Severity = severity,
            Status = BugStatus.Open,
            Priority = priority,
            AssignedTo = assignedTo,
            FoundBy = foundBy,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(string? title, string? description, string? stepsToReproduce, string? expectedBehavior, string? actualBehavior, BugSeverity? severity, TaskPriority? priority)
    {
        Title = title ?? Title;
        Description = description ?? Description;
        StepsToReproduce = stepsToReproduce ?? StepsToReproduce;
        ExpectedBehavior = expectedBehavior ?? ExpectedBehavior;
        ActualBehavior = actualBehavior ?? ActualBehavior;
        Severity = severity ?? Severity;
        Priority = priority ?? Priority;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AssignTo(Guid employeeId)
    {
        AssignedTo = employeeId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeStatus(BugStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }
}
