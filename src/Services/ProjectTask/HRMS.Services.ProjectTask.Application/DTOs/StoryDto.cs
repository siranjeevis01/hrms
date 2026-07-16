using HRMS.Services.ProjectTask.Domain.Enums;

namespace HRMS.Services.ProjectTask.Application.DTOs;

public class StoryDto
{
    public Guid Id { get; set; }
    public Guid EpicId { get; set; }
    public Guid ProjectId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? AcceptanceCriteria { get; set; }
    public int StoryPoints { get; set; }
    public StoryStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    public Guid? AssignedTo { get; set; }
    public Guid? SprintId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
