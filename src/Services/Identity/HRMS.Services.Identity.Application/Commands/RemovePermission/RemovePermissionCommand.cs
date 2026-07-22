using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.RemovePermission;

public record RemovePermissionCommand(
    Guid RoleId,
    string Permission) : IRequest<Result>;
