using HRMS.Services.ProjectTask.Application.Commands.CreateComment;
using HRMS.Services.ProjectTask.Application.Commands.UpdateComment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.ProjectTask.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentCommand command)
    {
        var id = await _mediator.Send(command);
        return Created(string.Empty, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateComment(Guid id, [FromBody] UpdateCommentCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
