using HRMS.Services.Recruitment.Application.Commands.AcceptOffer;
using HRMS.Services.Recruitment.Application.Commands.CreateOffer;
using HRMS.Services.Recruitment.Application.Commands.RejectOffer;
using HRMS.Services.Recruitment.Application.Commands.SendOffer;
using HRMS.Services.Recruitment.Application.DTOs;
using HRMS.Services.Recruitment.Application.Queries.GetOfferLetters;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Recruitment.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OfferLettersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OfferLettersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<OfferLetterDto>), 200)]
    public async Task<IActionResult> GetOfferLetters(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Guid? candidateId = null,
        [FromQuery] Guid? tenantId = null)
    {
        var query = new GetOfferLettersQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            CandidateId = candidateId,
            TenantId = tenantId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateOffer([FromBody] CreateOfferCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPost("{id:guid}/send")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> SendOffer(Guid id, [FromBody] SendOfferCommand command)
    {
        command.OfferLetterId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/accept")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> AcceptOffer(Guid id)
    {
        await _mediator.Send(new AcceptOfferCommand { OfferLetterId = id });
        return NoContent();
    }

    [HttpPost("{id:guid}/reject")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> RejectOffer(Guid id, [FromBody] RejectOfferCommand command)
    {
        command.OfferLetterId = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
