using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.AddPermission;

public record AddPermissionCommand(
    Guid RoleId,
    string Permission,
    string? Module,
    string? Description) : IRequest<Result>;
