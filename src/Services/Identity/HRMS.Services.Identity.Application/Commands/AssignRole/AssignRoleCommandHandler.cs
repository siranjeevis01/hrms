using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.Events;
using HRMS.Services.Identity.Application.Interfaces;
using HRMS.Services.Identity.Domain.Entities;
using MediatR;
using UserRole = HRMS.Services.Identity.Domain.Entities.UserRole;

namespace HRMS.Services.Identity.Application.Commands.AssignRole;

public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, Result>
{
    private readonly IIdentityDbContext _context;
    private readonly IPublisher _publisher;

    public AssignRoleCommandHandler(IIdentityDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task<Result> Handle(
        AssignRoleCommand request,
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

        var existingRoles = await _context.GetUserRoleNamesAsync(request.UserId, cancellationToken);

        if (existingRoles.Contains(role.Name))
        {
            return Result.Failure(
                Error.Conflict("Role.AlreadyAssigned", $"User already has the '{role.Name}' role."));
        }

        var userRole = UserRole.Create(request.UserId, request.RoleId, request.AssignedBy);
        _context.AddUserRole(userRole);

        await _context.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(
            new RoleAssignedEvent(request.UserId, request.RoleId, request.AssignedBy),
            cancellationToken);

        return Result.Success();
    }
}
