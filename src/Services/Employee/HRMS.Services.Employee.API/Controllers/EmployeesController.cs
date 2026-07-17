using HRMS.Services.Employee.Application.Commands.AddBankDetail;
using HRMS.Services.Employee.Application.Commands.AddCertification;
using HRMS.Services.Employee.Application.Commands.AddDependent;
using HRMS.Services.Employee.Application.Commands.AddEducation;
using HRMS.Services.Employee.Application.Commands.AddEmergencyContact;
using HRMS.Services.Employee.Application.Commands.AddSkill;
using HRMS.Services.Employee.Application.Commands.AddWorkExperience;
using HRMS.Services.Employee.Application.Commands.ChangeEmployeeStatus;
using HRMS.Services.Employee.Application.Commands.CreateEmployee;
using HRMS.Services.Employee.Application.Commands.PromoteEmployee;
using HRMS.Services.Employee.Application.Commands.TerminateEmployee;
using HRMS.Services.Employee.Application.Commands.TransferEmployee;
using HRMS.Services.Employee.Application.Commands.UpdateEmployee;
using HRMS.Services.Employee.Application.Commands.UpdateEmployeePersonalInfo;
using HRMS.Services.Employee.Application.Commands.UploadDocument;
using HRMS.Services.Employee.Application.Commands.VerifyDocument;
using HRMS.Services.Employee.Application.Queries.GetDepartmentHeadCount;
using HRMS.Services.Employee.Application.Queries.GetEmployee;
using HRMS.Services.Employee.Application.Queries.GetEmployees;
using HRMS.Services.Employee.Application.Queries.GetEmployeesLeaving;
using HRMS.Services.Employee.Application.Queries.GetNewHires;
using HRMS.Services.Employee.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Employee.API.Controllers;

[ApiController]
[Route("api/employees/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<EmployeeListDto>), 200)]
    public async Task<IActionResult> GetEmployees(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Guid? departmentId = null,
        [FromQuery] Guid? designationId = null,
        [FromQuery] Domain.Enums.EmploymentStatus? status = null,
        [FromQuery] string? searchTerm = null)
    {
        var query = new GetEmployeesQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            DepartmentId = departmentId,
            DesignationId = designationId,
            Status = status,
            SearchTerm = searchTerm
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(EmployeeDto), 200)]
    public async Task<IActionResult> GetEmployee(Guid id)
    {
        var result = await _mediator.Send(new GetEmployeeQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetEmployee), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] UpdateEmployeeCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id:guid}/personal-info")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdatePersonalInfo(Guid id, [FromBody] UpdateEmployeePersonalInfoCommand command)
    {
        command.EmployeeId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/promote")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> PromoteEmployee(Guid id, [FromBody] PromoteEmployeeCommand command)
    {
        command.EmployeeId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/transfer")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> TransferEmployee(Guid id, [FromBody] TransferEmployeeCommand command)
    {
        command.EmployeeId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/terminate")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> TerminateEmployee(Guid id, [FromBody] TerminateEmployeeCommand command)
    {
        command.EmployeeId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id:guid}/status")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] ChangeEmployeeStatusCommand command)
    {
        command.EmployeeId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("new-hires")]
    [ProducesResponseType(typeof(List<EmployeeListDto>), 200)]
    public async Task<IActionResult> GetNewHires(
        [FromQuery] DateTime fromDate,
        [FromQuery] DateTime toDate,
        [FromQuery] Guid? departmentId = null)
    {
        var result = await _mediator.Send(new GetNewHiresQuery
        {
            FromDate = fromDate,
            ToDate = toDate,
            DepartmentId = departmentId
        });
        return Ok(result);
    }

    [HttpGet("leaving")]
    [ProducesResponseType(typeof(List<EmployeeListDto>), 200)]
    public async Task<IActionResult> GetEmployeesLeaving([FromQuery] Guid? departmentId = null)
    {
        var result = await _mediator.Send(new GetEmployeesLeavingQuery { DepartmentId = departmentId });
        return Ok(result);
    }

    [HttpGet("department-headcount")]
    [ProducesResponseType(typeof(List<HRMS.Services.Employee.Application.Queries.GetDepartmentHeadCount.DepartmentHeadCountDto>), 200)]
    public async Task<IActionResult> GetDepartmentHeadCount([FromQuery] Guid? companyId = null)
    {
        var result = await _mediator.Send(new GetDepartmentHeadCountQuery { CompanyId = companyId });
        return Ok(result);
    }
}
