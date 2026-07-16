using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.DTOs;
using HRMS.Services.Identity.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Identity.Application.Queries.GetUserRoles;

public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, Result<IReadOnlyList<RoleDto>>>
{
    private readonly IIdentityDbContext _context;

    public GetUserRolesQueryHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IReadOnlyList<RoleDto>>> Handle(
        GetUserRolesQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _context.FindUserByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result<IReadOnlyList<RoleDto>>.Failure(
                Error.NotFound("User.NotFound", "User not found."));
        }

        var roles = await _context.UserRoles
            .Where(ur => ur.UserId == request.UserId)
            .Join(
                _context.Roles,
                ur => ur.RoleId,
                r => r.Id,
                (ur, r) => r)
            .ToListAsync(cancellationToken);

        var roleDtos = roles.Select(role => new RoleDto
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            Permissions = role.Permissions.Select(p => p.Permission).ToList()
        }).ToList();

        return Result<IReadOnlyList<RoleDto>>.Success(roleDtos);
    }
}
