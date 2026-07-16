using TaskStatus = HRMS.Services.ProjectTask.Domain.Enums.TaskStatus;
using HRMS.Services.ProjectTask.Domain.Enums;

namespace HRMS.Services.ProjectTask.Application.DTOs;

public class TaskItemDto
{
    public Guid Id { get; set; }
    public Guid? StoryId { get; set; }
    public Guid ProjectId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    public Guid? AssignedTo { get; set; }
    public decimal EstimatedHours { get; set; }
    public decimal LoggedHours { get; set; }
    public DateTime? DueDate { get; set; }
    public Guid? ParentTaskId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
