using HRMS.Services.Identity.Application.DTOs;
using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Identity.Application.Queries.GetAllRoles;

public record GetAllRolesQuery() : IRequest<Result<IReadOnlyList<RoleDto>>>;
