using HRMS.Services.Helpdesk.Application.Commands.AddTicketComment;
using HRMS.Services.Helpdesk.Application.Queries.GetTicketComments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Helpdesk.API.Controllers;

[ApiController]
[Route("api/tickets/{ticketId:guid}/comments")]
public class TicketCommentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TicketCommentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<TicketCommentDto>), 200)]
    public async Task<IActionResult> GetTicketComments(Guid ticketId)
    {
        var result = await _mediator.Send(new GetTicketCommentsQuery { TicketId = ticketId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> AddTicketComment(Guid ticketId, [FromBody] AddTicketCommentCommand command)
    {
        command.TicketId = ticketId;
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTicketComments), new { ticketId }, id);
    }
}
