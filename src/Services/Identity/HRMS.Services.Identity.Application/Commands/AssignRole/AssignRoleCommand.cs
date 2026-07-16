using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.AssignRole;

public record AssignRoleCommand(
    Guid UserId,
    Guid RoleId,
    Guid? AssignedBy) : IRequest<Result>;
