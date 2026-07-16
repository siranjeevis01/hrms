namespace HRMS.Services.ProjectTask.Application.DTOs;

public class CommentDto
{
    public Guid Id { get; set; }
    public Guid? TaskItemId { get; set; }
    public Guid? StoryId { get; set; }
    public Guid? BugId { get; set; }
    public Guid EmployeeId { get; set; }
    public string Content { get; set; } = string.Empty;
    public Guid? ParentCommentId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
