using HRMS.Services.ProjectTask.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.ProjectTask.Domain.Entities;

public class TaskItem : BaseEntity
{
    public Guid? StoryId { get; private set; }
    public Guid ProjectId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public new Enums.TaskStatus Status { get; private set; }
    public TaskPriority Priority { get; private set; }
    public Guid? AssignedTo { get; private set; }
    public decimal EstimatedHours { get; private set; }
    public decimal LoggedHours { get; private set; }
    public DateTime? DueDate { get; private set; }
    public Guid? ParentTaskId { get; private set; }

    private TaskItem() { }

    public static TaskItem Create(
        Guid? storyId,
        Guid projectId,
        string title,
        string? description,
        TaskPriority priority,
        Guid? assignedTo,
        decimal estimatedHours,
        DateTime? dueDate,
        Guid? parentTaskId,
        Guid tenantId)
    {
        return new TaskItem
        {
            Id = Guid.NewGuid(),
            StoryId = storyId,
            ProjectId = projectId,
            Title = title,
            Description = description,
            Status = Enums.TaskStatus.Todo,
            Priority = priority,
            AssignedTo = assignedTo,
            EstimatedHours = estimatedHours,
            LoggedHours = 0,
            DueDate = dueDate,
            ParentTaskId = parentTaskId,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(string? title, string? description, TaskPriority? priority, decimal? estimatedHours, DateTime? dueDate, Guid? parentTaskId)
    {
        Title = title ?? Title;
        Description = description ?? Description;
        Priority = priority ?? Priority;
        EstimatedHours = estimatedHours ?? EstimatedHours;
        DueDate = dueDate ?? DueDate;
        ParentTaskId = parentTaskId ?? ParentTaskId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AssignTo(Guid employeeId)
    {
        AssignedTo = employeeId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeStatus(Enums.TaskStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddLoggedHours(decimal hours)
    {
        LoggedHours += hours;
        UpdatedAt = DateTime.UtcNow;
    }
}
