using HRMS.Services.Recruitment.Application.Commands.SubmitInterviewFeedback;
using HRMS.Services.Recruitment.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Recruitment.API.Controllers;

[ApiController]
[Route("api/recruitment/[controller]")]
public class InterviewFeedbacksController : ControllerBase
{
    private readonly IMediator _mediator;

    public InterviewFeedbacksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> SubmitFeedback([FromBody] SubmitInterviewFeedbackCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }
}
