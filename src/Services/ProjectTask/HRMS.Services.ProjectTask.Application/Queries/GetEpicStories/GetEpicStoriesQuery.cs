using HRMS.Services.ProjectTask.Application.DTOs;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Queries.GetEpicStories;

public class GetEpicStoriesQuery : IRequest<List<StoryDto>>
{
    public Guid EpicId { get; set; }
}
