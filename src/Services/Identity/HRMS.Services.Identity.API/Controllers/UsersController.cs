using System.Security.Claims;
using HRMS.Services.Identity.Application.Commands.AssignRole;
using HRMS.Services.Identity.Application.Commands.DeactivateUser;
using HRMS.Services.Identity.Application.Commands.RemoveRole;
using HRMS.Services.Identity.Application.Commands.UpdateUserProfile;
using HRMS.Services.Identity.Application.DTOs;
using HRMS.Services.Identity.Application.Queries.GetAuditLogs;
using HRMS.Services.Identity.Application.Queries.GetCurrentUser;
using HRMS.Services.Identity.Application.Queries.GetUserById;
using HRMS.Services.Identity.Application.Queries.GetUserRoles;
using HRMS.Services.Identity.Application.Queries.GetUsers;
using HRMS.Services.Identity.Application.Queries.GetUserSessions;
using HRMS.Shared.Kernel.Common;
using HRMS.Shared.Kernel.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Identity.API.Controllers;

[ApiController]
[Route("api/identity/[controller]")]
[Authorize]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IMediator mediator, ILogger<UsersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [Authorize(Policy = "HRAdmin")]
    [ProducesResponseType(typeof(PagedResult<UserDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsers(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? searchTerm = null,
        [FromQuery] string? sortBy = null,
        [FromQuery] SortOrder sortOrder = SortOrder.Ascending,
        CancellationToken cancellationToken = default)
    {
        var tenantId = GetTenantId();
        var pagination = new PaginationRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            SearchTerm = searchTerm,
            SortBy = sortBy,
            SortOrder = sortOrder
        };

        var query = new GetUsersQuery(pagination, tenantId);
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
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);
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

    [HttpGet("me")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        if (userId is null)
            return Unauthorized();

        var query = new GetCurrentUserQuery(userId.Value);
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

    [HttpPut("me")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCurrentUser(
        [FromBody] UpdateProfileRequest request,
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        if (userId is null)
            return Unauthorized();

        var command = new UpdateUserProfileCommand(
            userId.Value,
            request.FirstName,
            request.LastName,
            request.PhoneNumber,
            request.ProfilePictureUrl);

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

    [HttpPut("{id:guid}/deactivate")]
    [Authorize(Policy = "HRAdmin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeactivateUser(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeactivateUserCommand(id);
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

    [HttpGet("{id:guid}/sessions")]
    [ProducesResponseType(typeof(IReadOnlyList<UserSessionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserSessions(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetUserSessionsQuery(id);
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

    [HttpGet("{id:guid}/roles")]
    [Authorize(Policy = "HRAdmin")]
    [ProducesResponseType(typeof(IReadOnlyList<RoleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserRoles(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetUserRolesQuery(id);
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

    [HttpPost("{id:guid}/roles")]
    [Authorize(Policy = "HRAdmin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AssignRole(
        Guid id,
        [FromBody] AssignRoleRequest request,
        CancellationToken cancellationToken)
    {
        var currentUserId = GetUserId();
        var command = new AssignRoleCommand(id, request.RoleId, currentUserId);
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

    [HttpDelete("{id:guid}/roles/{roleId:guid}")]
    [Authorize(Policy = "HRAdmin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveRole(
        Guid id,
        Guid roleId,
        CancellationToken cancellationToken)
    {
        var command = new RemoveRoleCommand(id, roleId);
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

    [HttpGet("audit-logs")]
    [Authorize(Policy = "HRAdmin")]
    [ProducesResponseType(typeof(PagedResult<AuditLogDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAuditLogs(
        [FromQuery] Guid? userId = null,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken cancellationToken = default)
    {
        var pagination = new PaginationRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var query = new GetAuditLogsQuery(userId, startDate, endDate, pagination);
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

    private Guid? GetUserId()
    {
        var sub = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                  ?? User.FindFirst("sub")?.Value;
        return sub is not null && Guid.TryParse(sub, out var userId) ? userId : null;
    }

    private Guid? GetTenantId()
    {
        var tenantClaim = User.FindFirst("tenant_id")?.Value;
        return tenantClaim is not null && Guid.TryParse(tenantClaim, out var tenantId) ? tenantId : null;
    }
}

#region Request DTOs

public record UpdateProfileRequest(
    string FirstName,
    string LastName,
    string? PhoneNumber,
    string? ProfilePictureUrl);

public record AssignRoleRequest(Guid RoleId);

#endregion
