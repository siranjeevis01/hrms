using HRMS.Services.ProjectTask.Application.DTOs;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Queries.GetTaskComments;

public class GetTaskCommentsQuery : IRequest<List<CommentDto>>
{
    public Guid? TaskItemId { get; set; }
    public Guid? StoryId { get; set; }
    public Guid? BugId { get; set; }
}
