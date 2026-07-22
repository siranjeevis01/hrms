using HRMS.Services.Identity.Application.Interfaces;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Identity.Application.Commands.UpdateRole;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Result>
{
    private readonly IIdentityDbContext _context;

    public UpdateRoleCommandHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(
        UpdateRoleCommand request,
        CancellationToken cancellationToken)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
        if (role is null)
            return Result.Failure(Error.NotFound("Role.NotFound", $"Role with ID {request.Id} not found."));

        role.UpdateDescription(request.Description);
        _context.UpdateRole(role);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
