using HRMS.Services.ProjectTask.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.ProjectTask.Domain.Entities;

public class Epic : BaseEntity
{
    public Guid ProjectId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public new StoryStatus Status { get; private set; }
    public TaskPriority Priority { get; private set; }
    public DateTime? StartDate { get; private set; }
    public DateTime? TargetDate { get; private set; }
    public decimal ProgressPercentage { get; private set; }

    private readonly List<Story> _stories = new();
    public IReadOnlyCollection<Story> Stories => _stories.AsReadOnly();

    private Epic() { }

    public static Epic Create(
        Guid projectId,
        string title,
        string? description,
        TaskPriority priority,
        DateTime? startDate,
        DateTime? targetDate,
        Guid tenantId)
    {
        return new Epic
        {
            Id = Guid.NewGuid(),
            ProjectId = projectId,
            Title = title,
            Description = description,
            Status = StoryStatus.Draft,
            Priority = priority,
            StartDate = startDate,
            TargetDate = targetDate,
            ProgressPercentage = 0,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(string? title, string? description, TaskPriority? priority, DateTime? startDate, DateTime? targetDate)
    {
        Title = title ?? Title;
        Description = description ?? Description;
        Priority = priority ?? Priority;
        StartDate = startDate ?? StartDate;
        TargetDate = targetDate ?? TargetDate;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateProgress(decimal percentage)
    {
        ProgressPercentage = Math.Clamp(percentage, 0, 100);
        UpdatedAt = DateTime.UtcNow;
    }
}
