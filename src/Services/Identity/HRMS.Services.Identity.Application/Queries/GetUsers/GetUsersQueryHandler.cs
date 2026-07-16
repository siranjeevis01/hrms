using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.DTOs;
using HRMS.Services.Identity.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Identity.Application.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<PagedResult<UserDto>>>
{
    private readonly IIdentityDbContext _context;

    public GetUsersQueryHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PagedResult<UserDto>>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Users.AsQueryable();

        if (request.TenantId.HasValue)
        {
            query = query.Where(u => u.TenantId == request.TenantId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Pagination.SearchTerm))
        {
            var searchTerm = request.Pagination.SearchTerm.ToLowerInvariant();
            query = query.Where(u =>
                u.Email.Contains(searchTerm) ||
                u.FirstName.Contains(searchTerm) ||
                u.LastName.Contains(searchTerm));
        }

        query = request.Pagination.SortBy?.ToLowerInvariant() switch
        {
            "email" => request.Pagination.SortOrder == Shared.Kernel.Enums.SortOrder.Descending
                ? query.OrderByDescending(u => u.Email)
                : query.OrderBy(u => u.Email),
            "firstname" => request.Pagination.SortOrder == Shared.Kernel.Enums.SortOrder.Descending
                ? query.OrderByDescending(u => u.FirstName)
                : query.OrderBy(u => u.FirstName),
            "lastname" => request.Pagination.SortOrder == Shared.Kernel.Enums.SortOrder.Descending
                ? query.OrderByDescending(u => u.LastName)
                : query.OrderBy(u => u.LastName),
            "createdat" => request.Pagination.SortOrder == Shared.Kernel.Enums.SortOrder.Descending
                ? query.OrderByDescending(u => u.CreatedAt)
                : query.OrderBy(u => u.CreatedAt),
            _ => query.OrderByDescending(u => u.CreatedAt)
        };

        var totalCount = await query.CountAsync(cancellationToken);

        var users = await query
            .Skip(request.Pagination.Skip)
            .Take(request.Pagination.PageSize)
            .ToListAsync(cancellationToken);

        var userDtos = new List<UserDto>();

        foreach (var user in users)
        {
            var roles = await _context.GetUserRoleNamesAsync(user.Id, cancellationToken);
            var permissions = await _context.GetUserPermissionsAsync(user.Id, cancellationToken);

            userDtos.Add(new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                ProfilePictureUrl = user.ProfilePictureUrl,
                IsMfaEnabled = user.IsMfaEnabled,
                Roles = roles,
                Permissions = permissions,
                CreatedAt = user.CreatedAt
            });
        }

        var pagedResult = PagedResult<UserDto>.Create(
            userDtos,
            totalCount,
            request.Pagination.PageNumber,
            request.Pagination.PageSize);

        return Result<PagedResult<UserDto>>.Success(pagedResult);
    }
}
