using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.DTOs;
using MediatR;

namespace HRMS.Services.Identity.Application.Queries.GetUsers;

public record GetUsersQuery(
    PaginationRequest Pagination,
    Guid? TenantId) : IRequest<Result<PagedResult<UserDto>>>;
