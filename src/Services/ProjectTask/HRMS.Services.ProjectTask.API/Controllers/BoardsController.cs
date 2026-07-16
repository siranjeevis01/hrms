using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Queries.GetBoard;
using HRMS.Services.ProjectTask.Domain.Entities;
using HRMS.Services.ProjectTask.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.ProjectTask.API.Controllers;

[ApiController]
[Route("api/projects/{projectId:guid}/boards")]
public class BoardsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BoardsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(BoardDto), 200)]
    public async Task<IActionResult> GetBoard(Guid projectId)
    {
        var result = await _mediator.Send(new GetBoardQuery { ProjectId = projectId });
        if (result == null) return NotFound();
        return Ok(result);
    }
}
