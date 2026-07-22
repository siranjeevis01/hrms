using HRMS.Services.Identity.Application.Interfaces;
using HRMS.Services.Identity.Domain.Entities;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Identity.Application.Commands.AddPermission;

public class AddPermissionCommandHandler : IRequestHandler<AddPermissionCommand, Result>
{
    private readonly IIdentityDbContext _context;

    public AddPermissionCommandHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(
        AddPermissionCommand request,
        CancellationToken cancellationToken)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == request.RoleId, cancellationToken);
        if (role is null)
            return Result.Failure(Error.NotFound("Role.NotFound", $"Role with ID {request.RoleId} not found."));

        var exists = await _context.RolePermissions.AnyAsync(
            rp => rp.RoleId == request.RoleId && rp.Permission == request.Permission.Trim(),
            cancellationToken);

        if (exists)
            return Result.Failure(Error.Conflict("Permission.Duplicate", $"Permission '{request.Permission}' already exists for this role."));

        var permission = RolePermission.Create(
            Guid.NewGuid(),
            request.RoleId,
            request.Permission,
            request.Module,
            request.Description);

        _context.AddRolePermission(permission);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
