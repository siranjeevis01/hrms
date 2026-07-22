using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.UpdateRole;

public record UpdateRoleCommand(
    Guid Id,
    string? Description) : IRequest<Result>;
