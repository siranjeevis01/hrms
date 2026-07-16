using HRMS.Services.Identity.Domain.Entities;
using UserRole = HRMS.Services.Identity.Domain.Entities.UserRole;

namespace HRMS.Services.Identity.Application.Interfaces;

public interface IIdentityDbContext
{
    IQueryable<ApplicationUser> Users { get; }
    IQueryable<Role> Roles { get; }
    IQueryable<UserRole> UserRoles { get; }
    IQueryable<UserPermission> UserPermissions { get; }
    IQueryable<RolePermission> RolePermissions { get; }
    IQueryable<RefreshToken> RefreshTokens { get; }
    IQueryable<UserSession> UserSessions { get; }
    IQueryable<AuditLog> AuditLogs { get; }

    Task<ApplicationUser?> FindUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ApplicationUser?> FindUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<Role?> FindRoleByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Role?> FindRoleByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<RefreshToken?> FindRefreshTokenAsync(string token, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RefreshToken>> GetActiveRefreshTokensByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<UserSession?> FindSessionByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<UserSession>> GetActiveSessionsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<string>> GetUserRoleNamesAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<string>> GetUserPermissionsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<string?> GetUserPasswordHashAsync(Guid userId, CancellationToken cancellationToken = default);
    Task SetUserPasswordHashAsync(Guid userId, string hashedPassword, CancellationToken cancellationToken = default);

    void AddUser(ApplicationUser user);
    void AddRefreshToken(RefreshToken refreshToken);
    void AddUserSession(UserSession session);
    void AddAuditLog(AuditLog auditLog);
    void AddUserRole(UserRole userRole);
    void RemoveUserRole(UserRole userRole);
    void UpdateUser(ApplicationUser user);
    void UpdateRefreshToken(RefreshToken refreshToken);
    void UpdateSession(UserSession session);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
