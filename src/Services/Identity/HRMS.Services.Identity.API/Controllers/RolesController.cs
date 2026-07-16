using System.Security.Claims;
using HRMS.Services.Identity.Application.DTOs;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Identity.API.Controllers;

[ApiController]
[Route("api/identity/[controller]")]
[Authorize(Policy = "HRAdmin")]
[Produces("application/json")]
public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RolesController> _logger;

    public RolesController(IMediator mediator, ILogger<RolesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<RoleDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
    {
        var query = new GetAllRolesQuery();
        var result = await _mediator.Send(query, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new ApiErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.Message,
                Details = result.Error.Code
            });

        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRoleById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetRoleByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        if (!result.IsSuccess)
            return NotFound(new ApiErrorResponse
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = result.Error.Message,
                Details = result.Error.Code
            });

        return Ok(result.Value);
    }

    [HttpPost]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateRole(
        [FromBody] CreateRoleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateRoleCommand(request.Name, request.Description);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new ApiErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.Message,
                Details = result.Error.Code
            });

        return CreatedAtAction(nameof(GetRoleById), new { id = result.Value.Id }, result.Value);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateRole(
        Guid id,
        [FromBody] UpdateRoleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateRoleCommand(id, request.Description);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new ApiErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.Message,
                Details = result.Error.Code
            });

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteRole(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteRoleCommand(id);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new ApiErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.Message,
                Details = result.Error.Code
            });

        return NoContent();
    }

    [HttpPost("{id:guid}/permissions")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddPermission(
        Guid id,
        [FromBody] AddPermissionRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddPermissionCommand(id, request.Permission, request.Module, request.Description);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new ApiErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.Message,
                Details = result.Error.Code
            });

        return NoContent();
    }

    [HttpDelete("{id:guid}/permissions/{permission}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemovePermission(
        Guid id,
        string permission,
        CancellationToken cancellationToken)
    {
        var command = new RemovePermissionCommand(id, permission);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new ApiErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.Message,
                Details = result.Error.Code
            });

        return NoContent();
    }
}

#region Role Management Commands

public record GetAllRolesQuery() : IRequest<Result<IReadOnlyList<RoleDto>>>;

public record GetRoleByIdQuery(Guid Id) : IRequest<Result<RoleDto>>;

public record CreateRoleCommand(
    string Name,
    string? Description) : IRequest<Result<RoleDto>>;

public record UpdateRoleCommand(
    Guid Id,
    string? Description) : IRequest<Result>;

public record DeleteRoleCommand(Guid Id) : IRequest<Result>;

public record AddPermissionCommand(
    Guid RoleId,
    string Permission,
    string? Module,
    string? Description) : IRequest<Result>;

public record RemovePermissionCommand(
    Guid RoleId,
    string Permission) : IRequest<Result>;

#endregion

#region Request DTOs

public record CreateRoleRequest(string Name, string? Description);

public record UpdateRoleRequest(string? Description);

public record AddPermissionRequest(
    string Permission,
    string? Module,
    string? Description);

#endregion
