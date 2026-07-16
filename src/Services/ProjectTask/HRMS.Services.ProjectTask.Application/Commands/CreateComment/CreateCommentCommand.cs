using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.CreateComment;

public class CreateCommentCommand : IRequest<Guid>
{
    public Guid? TaskItemId { get; set; }
    public Guid? StoryId { get; set; }
    public Guid? BugId { get; set; }
    public Guid EmployeeId { get; set; }
    public string Content { get; set; } = string.Empty;
    public Guid? ParentCommentId { get; set; }
    public Guid TenantId { get; set; }
}
