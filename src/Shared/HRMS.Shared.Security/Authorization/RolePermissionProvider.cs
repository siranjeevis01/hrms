using HRMS.Shared.Kernel.Enums;

namespace HRMS.Shared.Security.Authorization;

public interface IRolePermissionProvider
{
    IReadOnlyList<string> GetPermissionsForRole(string role);
    IReadOnlyList<string> GetAllPermissions();
}

public class RolePermissionProvider : IRolePermissionProvider
{
    private readonly Dictionary<string, IReadOnlyList<string>> _rolePermissions = new()
    {
        [UserRole.SuperAdmin.ToString()] = new List<string>
        {
            "employees.view", "employees.create", "employees.edit", "employees.delete",
            "departments.view", "departments.create", "departments.edit", "departments.delete",
            "attendance.view", "attendance.manage",
            "leave.view", "leave.approve", "leave.manage",
            "payroll.view", "payroll.process", "payroll.manage",
            "reports.view", "reports.generate", "reports.export",
            "settings.view", "settings.manage",
            "users.view", "users.create", "users.edit", "users.delete",
            "roles.manage",
            "tenants.manage"
        },
        [UserRole.HRAdmin.ToString()] = new List<string>
        {
            "employees.view", "employees.create", "employees.edit", "employees.delete",
            "departments.view", "departments.create", "departments.edit",
            "attendance.view", "attendance.manage",
            "leave.view", "leave.approve", "leave.manage",
            "payroll.view", "payroll.process",
            "reports.view", "reports.generate", "reports.export",
            "settings.view", "settings.manage",
            "users.view", "users.create", "users.edit"
        },
        [UserRole.HRManager.ToString()] = new List<string>
        {
            "employees.view", "employees.create", "employees.edit",
            "departments.view", "departments.create", "departments.edit",
            "attendance.view", "attendance.manage",
            "leave.view", "leave.approve",
            "payroll.view",
            "reports.view", "reports.generate",
            "users.view", "users.create"
        },
        [UserRole.Manager.ToString()] = new List<string>
        {
            "employees.view",
            "departments.view",
            "attendance.view",
            "leave.view", "leave.approve",
            "reports.view",
            "users.view"
        },
        [UserRole.Employee.ToString()] = new List<string>
        {
            "employees.view",
            "attendance.view",
            "leave.view", "leave.apply",
            "reports.view"
        },
        [UserRole.ReadOnly.ToString()] = new List<string>
        {
            "employees.view",
            "departments.view",
            "attendance.view",
            "leave.view",
            "reports.view"
        }
    };

    public IReadOnlyList<string> GetPermissionsForRole(string role)
    {
        return _rolePermissions.TryGetValue(role, out var permissions)
            ? permissions
            : Array.Empty<string>();
    }

    public IReadOnlyList<string> GetAllPermissions()
    {
        return _rolePermissions
            .SelectMany(kvp => kvp.Value)
            .Distinct()
            .OrderBy(p => p)
            .ToList();
    }
}
