using HRMS.Services.ProjectTask.Domain.Enums;

namespace HRMS.Services.ProjectTask.Application.DTOs;

public class BugDto
{
    public Guid Id { get; set; }
    public Guid? StoryId { get; set; }
    public Guid ProjectId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? StepsToReproduce { get; set; }
    public string? ExpectedBehavior { get; set; }
    public string? ActualBehavior { get; set; }
    public BugSeverity Severity { get; set; }
    public BugStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    public Guid? AssignedTo { get; set; }
    public Guid? FoundBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
