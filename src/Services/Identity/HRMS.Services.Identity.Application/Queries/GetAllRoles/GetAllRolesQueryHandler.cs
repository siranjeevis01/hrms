using HRMS.Services.Identity.Application.DTOs;
using HRMS.Services.Identity.Application.Interfaces;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Identity.Application.Queries.GetAllRoles;

public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Result<IReadOnlyList<RoleDto>>>
{
    private readonly IIdentityDbContext _context;

    public GetAllRolesQueryHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IReadOnlyList<RoleDto>>> Handle(
        GetAllRolesQuery request,
        CancellationToken cancellationToken)
    {
        var roles = await _context.Roles
            .OrderBy(r => r.Name)
            .ToListAsync(cancellationToken);

        var dtos = roles.Select(r => new RoleDto
        {
            Id = r.Id,
            Name = r.Name,
            Description = r.Description,
            Permissions = _context.RolePermissions
                .Where(rp => rp.RoleId == r.Id)
                .Select(rp => rp.Permission)
                .ToList()
        }).ToList();

        return Result<IReadOnlyList<RoleDto>>.Success(dtos);
    }
}
