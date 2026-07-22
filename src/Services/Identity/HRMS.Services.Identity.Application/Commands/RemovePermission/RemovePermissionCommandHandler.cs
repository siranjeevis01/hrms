using HRMS.Services.Identity.Application.Interfaces;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Identity.Application.Commands.RemovePermission;

public class RemovePermissionCommandHandler : IRequestHandler<RemovePermissionCommand, Result>
{
    private readonly IIdentityDbContext _context;

    public RemovePermissionCommandHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(
        RemovePermissionCommand request,
        CancellationToken cancellationToken)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == request.RoleId, cancellationToken);
        if (role is null)
            return Result.Failure(Error.NotFound("Role.NotFound", $"Role with ID {request.RoleId} not found."));

        var permission = await _context.RolePermissions.FirstOrDefaultAsync(
            rp => rp.RoleId == request.RoleId && rp.Permission == request.Permission,
            cancellationToken);

        if (permission is null)
            return Result.Failure(Error.NotFound("Permission.NotFound", $"Permission '{request.Permission}' not found for this role."));

        _context.RemoveRolePermission(permission);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
