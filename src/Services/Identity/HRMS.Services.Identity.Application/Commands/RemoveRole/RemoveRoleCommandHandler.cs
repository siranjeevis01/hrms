using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Identity.Application.Commands.RemoveRole;

public class RemoveRoleCommandHandler : IRequestHandler<RemoveRoleCommand, Result>
{
    private readonly IIdentityDbContext _context;

    public RemoveRoleCommandHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(
        RemoveRoleCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.FindUserByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(Error.NotFound("User.NotFound", "User not found."));
        }

        var role = await _context.FindRoleByIdAsync(request.RoleId, cancellationToken);

        if (role is null)
        {
            return Result.Failure(Error.NotFound("Role.NotFound", "Role not found."));
        }

        var userRole = await _context.UserRoles
            .FirstOrDefaultAsync(
                ur => ur.UserId == request.UserId && ur.RoleId == request.RoleId,
                cancellationToken);

        if (userRole is null)
        {
            return Result.Failure(
                Error.NotFound("Role.NotAssigned", "This role is not assigned to the user."));
        }

        _context.RemoveUserRole(userRole);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
