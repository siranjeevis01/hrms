using HRMS.Services.Employee.Application.Commands.AddBankDetail;
using HRMS.Services.Employee.Application.Queries.GetEmployeeSalary;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Employee.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeBankDetailsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeBankDetailsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("employee/{id:guid}")]
    [ProducesResponseType(typeof(List<BankDetailDto>), 200)]
    public async Task<IActionResult> GetBankDetails(Guid id)
    {
        var result = await _mediator.Send(new GetEmployeeSalaryQuery { EmployeeId = id });
        return Ok(result);
    }

    [HttpPost("employee/{id:guid}")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> AddBankDetail(Guid id, [FromBody] AddBankDetailCommand command)
    {
        command.EmployeeId = id;
        var bankId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetBankDetails), new { id }, bankId);
    }

    [HttpPut("{bankId:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateBankDetail(Guid bankId)
    {
        return NoContent();
    }

    [HttpDelete("{bankId:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteBankDetail(Guid bankId)
    {
        return NoContent();
    }
}
