using Microsoft.AspNetCore.Authorization;

namespace HRMS.Shared.Security.Authorization;

public class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; }

    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }
}

public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IRolePermissionProvider _rolePermissionProvider;

    public PermissionHandler(IRolePermissionProvider rolePermissionProvider)
    {
        _rolePermissionProvider = rolePermissionProvider;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var roles = context.User.Claims
            .Where(c => c.Type == "role")
            .Select(c => c.Value)
            .ToList();

        foreach (var role in roles)
        {
            var permissions = _rolePermissionProvider.GetPermissionsForRole(role);
            if (permissions.Contains(requirement.Permission, StringComparer.OrdinalIgnoreCase))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
        }

        return Task.CompletedTask;
    }
}
