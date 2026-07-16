using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.DTOs;
using MediatR;

namespace HRMS.Services.Identity.Application.Queries.GetUserRoles;

public record GetUserRolesQuery(Guid UserId) : IRequest<Result<IReadOnlyList<RoleDto>>>;
