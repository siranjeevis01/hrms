using HRMS.Services.Identity.Application.DTOs;
using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.CreateRole;

public record CreateRoleCommand(
    string Name,
    string? Description) : IRequest<Result<RoleDto>>;
