using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.ProjectTask.Domain.Entities;

public class Comment : BaseEntity
{
    public Guid? TaskItemId { get; private set; }
    public Guid? StoryId { get; private set; }
    public Guid? BugId { get; private set; }
    public Guid EmployeeId { get; private set; }
    public string Content { get; private set; } = string.Empty;
    public Guid? ParentCommentId { get; private set; }

    private Comment() { }

    public static Comment Create(
        Guid? taskItemId,
        Guid? storyId,
        Guid? bugId,
        Guid employeeId,
        string content,
        Guid? parentCommentId,
        Guid tenantId)
    {
        return new Comment
        {
            Id = Guid.NewGuid(),
            TaskItemId = taskItemId,
            StoryId = storyId,
            BugId = bugId,
            EmployeeId = employeeId,
            Content = content,
            ParentCommentId = parentCommentId,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void UpdateContent(string content)
    {
        Content = content;
        UpdatedAt = DateTime.UtcNow;
    }
}
