using HRMS.Services.ProjectTask.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.ProjectTask.Domain.Entities;

public class Story : BaseEntity
{
    public Guid EpicId { get; private set; }
    public Guid ProjectId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string? AcceptanceCriteria { get; private set; }
    public int StoryPoints { get; private set; }
    public new StoryStatus Status { get; private set; }
    public TaskPriority Priority { get; private set; }
    public Guid? AssignedTo { get; private set; }
    public Guid? SprintId { get; private set; }

    private Story() { }

    public static Story Create(
        Guid epicId,
        Guid projectId,
        string title,
        string? description,
        string? acceptanceCriteria,
        int storyPoints,
        TaskPriority priority,
        Guid tenantId)
    {
        return new Story
        {
            Id = Guid.NewGuid(),
            EpicId = epicId,
            ProjectId = projectId,
            Title = title,
            Description = description,
            AcceptanceCriteria = acceptanceCriteria,
            StoryPoints = storyPoints,
            Status = StoryStatus.Draft,
            Priority = priority,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(string? title, string? description, string? acceptanceCriteria, int? storyPoints, TaskPriority? priority)
    {
        Title = title ?? Title;
        Description = description ?? Description;
        AcceptanceCriteria = acceptanceCriteria ?? AcceptanceCriteria;
        StoryPoints = storyPoints ?? StoryPoints;
        Priority = priority ?? Priority;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AssignTo(Guid employeeId)
    {
        AssignedTo = employeeId;
        UpdatedAt = DateTime.UtcNow;
    }
}
