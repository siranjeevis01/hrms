using HRMS.Services.Helpdesk.Application.Commands.AssignTicket;
using HRMS.Services.Helpdesk.Application.Commands.ChangeTicketPriority;
using HRMS.Services.Helpdesk.Application.Commands.CloseTicket;
using HRMS.Services.Helpdesk.Application.Commands.CreateTicket;
using HRMS.Services.Helpdesk.Application.Commands.ReopenTicket;
using HRMS.Services.Helpdesk.Application.Commands.ResolveTicket;
using HRMS.Services.Helpdesk.Application.Commands.UpdateTicket;
using HRMS.Services.Helpdesk.Application.Queries.GetAssignedTickets;
using HRMS.Services.Helpdesk.Application.Queries.GetEmployeeTickets;
using HRMS.Services.Helpdesk.Application.Queries.GetHelpdeskStats;
using HRMS.Services.Helpdesk.Application.Queries.GetTicket;
using HRMS.Services.Helpdesk.Application.Queries.GetTickets;
using HRMS.Services.Helpdesk.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Helpdesk.API.Controllers;

[ApiController]
[Route("api/helpdesk/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TicketsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<HelpdeskTicketDto>), 200)]
    public async Task<IActionResult> GetTickets(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Guid? employeeId = null,
        [FromQuery] Domain.Enums.TicketStatus? status = null,
        [FromQuery] Domain.Enums.TicketPriority? priority = null,
        [FromQuery] Domain.Enums.TicketCategoryType? category = null,
        [FromQuery] Guid? assignedTo = null,
        [FromQuery] string? searchTerm = null)
    {
        var query = new GetTicketsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            EmployeeId = employeeId,
            Status = status,
            Priority = priority,
            Category = category,
            AssignedTo = assignedTo,
            SearchTerm = searchTerm
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(HelpdeskTicketDto), 200)]
    public async Task<IActionResult> GetTicket(Guid id)
    {
        var result = await _mediator.Send(new GetTicketQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateTicket([FromBody] CreateTicketCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTicket), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateTicket(Guid id, [FromBody] UpdateTicketCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/assign")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> AssignTicket(Guid id, [FromBody] AssignTicketCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id:guid}/priority")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ChangePriority(Guid id, [FromBody] ChangeTicketPriorityCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/resolve")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ResolveTicket(Guid id, [FromBody] ResolveTicketCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/close")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> CloseTicket(Guid id)
    {
        await _mediator.Send(new CloseTicketCommand { Id = id });
        return NoContent();
    }

    [HttpPost("{id:guid}/reopen")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ReopenTicket(Guid id)
    {
        await _mediator.Send(new ReopenTicketCommand { Id = id });
        return NoContent();
    }

    [HttpGet("employee/{employeeId:guid}")]
    [ProducesResponseType(typeof(PagedResult<HelpdeskTicketDto>), 200)]
    public async Task<IActionResult> GetEmployeeTickets(
        Guid employeeId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Domain.Enums.TicketStatus? status = null)
    {
        var query = new GetEmployeeTicketsQuery
        {
            EmployeeId = employeeId,
            PageNumber = pageNumber,
            PageSize = pageSize,
            Status = status
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("assigned/{assignedTo:guid}")]
    [ProducesResponseType(typeof(PagedResult<HelpdeskTicketDto>), 200)]
    public async Task<IActionResult> GetAssignedTickets(
        Guid assignedTo,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Domain.Enums.TicketStatus? status = null)
    {
        var query = new GetAssignedTicketsQuery
        {
            AssignedTo = assignedTo,
            PageNumber = pageNumber,
            PageSize = pageSize,
            Status = status
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("stats")]
    [ProducesResponseType(typeof(HelpdeskStatsDto), 200)]
    public async Task<IActionResult> GetHelpdeskStats([FromQuery] string tenantId = "")
    {
        var result = await _mediator.Send(new GetHelpdeskStatsQuery { TenantId = tenantId });
        return Ok(result);
    }
}
