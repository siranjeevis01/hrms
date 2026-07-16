using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.RemoveRole;

public record RemoveRoleCommand(
    Guid UserId,
    Guid RoleId) : IRequest<Result>;
