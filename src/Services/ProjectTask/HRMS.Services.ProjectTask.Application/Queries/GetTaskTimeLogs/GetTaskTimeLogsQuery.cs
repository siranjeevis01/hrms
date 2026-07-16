using HRMS.Services.ProjectTask.Application.DTOs;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Queries.GetTaskTimeLogs;

public class GetTaskTimeLogsQuery : IRequest<List<TimeLogDto>>
{
    public Guid? TaskItemId { get; set; }
    public Guid? StoryId { get; set; }
}
