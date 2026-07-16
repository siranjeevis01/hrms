using HRMS.Services.ProjectTask.Application.DTOs;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Queries.GetBacklog;

public class GetBacklogQuery : IRequest<GetBacklogResult>
{
    public Guid ProjectId { get; set; }
}

public class GetBacklogResult
{
    public List<StoryDto> UnassignedStories { get; set; } = new();
    public List<TaskItemDto> UnassignedTasks { get; set; } = new();
}
