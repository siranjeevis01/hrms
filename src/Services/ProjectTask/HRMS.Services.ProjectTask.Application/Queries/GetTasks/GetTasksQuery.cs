using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Shared.Kernel.Common;
using MediatR;
using TaskStatus = HRMS.Services.ProjectTask.Domain.Enums.TaskStatus;

namespace HRMS.Services.ProjectTask.Application.Queries.GetTasks;

public class GetTasksQuery : IRequest<PagedResult<TaskItemDto>>
{
    public Guid? ProjectId { get; set; }
    public Guid? StoryId { get; set; }
    public Guid? AssignedTo { get; set; }
    public TaskStatus? Status { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
