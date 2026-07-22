using HRMS.Services.Identity.Application.Interfaces;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Identity.Application.Commands.DeleteRole;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Result>
{
    private readonly IIdentityDbContext _context;

    public DeleteRoleCommandHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(
        DeleteRoleCommand request,
        CancellationToken cancellationToken)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
        if (role is null)
            return Result.Failure(Error.NotFound("Role.NotFound", $"Role with ID {request.Id} not found."));

        if (role.IsSystemRole)
            return Result.Failure(Error.BadRequest("Role.IsSystemRole", "System roles cannot be deleted."));

        var permissions = await _context.RolePermissions
            .Where(rp => rp.RoleId == role.Id)
            .ToListAsync(cancellationToken);

        foreach (var p in permissions)
            _context.RemoveRolePermission(p);

        _context.RemoveRole(role);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
