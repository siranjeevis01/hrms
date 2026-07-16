using HRMS.Services.ProjectTask.Application.Commands.AssignStory;
using HRMS.Services.ProjectTask.Application.Commands.CreateStory;
using HRMS.Services.ProjectTask.Application.Commands.UpdateStory;
using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Queries.GetEpicStories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.ProjectTask.API.Controllers;

[ApiController]
[Route("api/epics/{epicId:guid}/stories")]
public class StoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public StoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<StoryDto>), 200)]
    public async Task<IActionResult> GetStories(Guid epicId)
    {
        var result = await _mediator.Send(new GetEpicStoriesQuery { EpicId = epicId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateStory(Guid epicId, [FromBody] CreateStoryCommand command)
    {
        command.EpicId = epicId;
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetStories), new { epicId }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateStory(Guid epicId, Guid id, [FromBody] UpdateStoryCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/assign")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> AssignStory(Guid epicId, Guid id, [FromBody] AssignStoryCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
