using HRMS.Shared.Kernel.Enums;

namespace HRMS.Shared.Kernel.Interfaces;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    Guid? TenantId { get; }
    string? Email { get; }
    string? FullName { get; }
    IReadOnlyList<UserRole> Roles { get; }
    bool IsAuthenticated { get; }
    bool IsInRole(UserRole role);
}
