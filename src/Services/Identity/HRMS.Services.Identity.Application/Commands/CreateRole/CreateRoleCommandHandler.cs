using HRMS.Services.Identity.Application.DTOs;
using HRMS.Services.Identity.Application.Interfaces;
using HRMS.Services.Identity.Domain.Entities;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Identity.Application.Commands.CreateRole;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result<RoleDto>>
{
    private readonly IIdentityDbContext _context;

    public CreateRoleCommandHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Result<RoleDto>> Handle(
        CreateRoleCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await _context.Roles.AnyAsync(r => r.Name == request.Name.Trim(), cancellationToken);
        if (exists)
            return Result<RoleDto>.Failure(Error.Conflict("Role.DuplicateName", $"A role with name '{request.Name}' already exists."));

        var role = Role.Create(Guid.NewGuid(), request.Name, request.Description);
        _context.AddRole(role);
        await _context.SaveChangesAsync(cancellationToken);

        var dto = new RoleDto
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            Permissions = Array.Empty<string>()
        };

        return Result<RoleDto>.Success(dto);
    }
}
