using HRMS.Services.Identity.Application.Interfaces;
using HRMS.Services.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

using IdentityUserRole = HRMS.Services.Identity.Domain.Entities.UserRole;

namespace HRMS.Api;

public class IdentityDbContextAdapter : IIdentityDbContext
{
    private readonly HrmsDbContext _context;

    public IdentityDbContextAdapter(HrmsDbContext context)
    {
        _context = context;
    }

    public IQueryable<ApplicationUser> Users => _context.ApplicationUsers.AsQueryable();
    public IQueryable<Role> Roles => _context.Roles.AsQueryable();
    public IQueryable<UserRole> UserRoles => _context.UserRoles.AsQueryable();
    public IQueryable<UserPermission> UserPermissions => _context.UserPermissions.AsQueryable();
    public IQueryable<RolePermission> RolePermissions => _context.RolePermissions.AsQueryable();
    public IQueryable<RefreshToken> RefreshTokens => _context.RefreshTokens.AsQueryable();
    public IQueryable<UserSession> UserSessions => _context.UserSessions.AsQueryable();
    public IQueryable<AuditLog> AuditLogs => _context.AuthAuditLogs.AsQueryable();

    public async Task<ApplicationUser?> FindUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.ApplicationUsers.FindAsync(new object[] { id }, cancellationToken);

    public async Task<ApplicationUser?> FindUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        => await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
        => await _context.ApplicationUsers.AnyAsync(u => u.Email == email, cancellationToken);

    public async Task<Role?> FindRoleByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.Roles.FindAsync(new object[] { id }, cancellationToken);

    public async Task<Role?> FindRoleByNameAsync(string name, CancellationToken cancellationToken = default)
        => await _context.Roles.FirstOrDefaultAsync(r => r.Name == name, cancellationToken);

    public async Task<RefreshToken?> FindRefreshTokenAsync(string token, CancellationToken cancellationToken = default)
        => await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken);

    public async Task<IReadOnlyList<RefreshToken>> GetActiveRefreshTokensByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        => await _context.RefreshTokens
            .Where(rt => rt.UserId == userId && rt.IsActive)
            .ToListAsync(cancellationToken);

    public async Task<UserSession?> FindSessionByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.UserSessions.FindAsync(new object[] { id }, cancellationToken);

    public async Task<IReadOnlyList<UserSession>> GetActiveSessionsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        => await _context.UserSessions
            .Where(s => s.UserId == userId && s.IsActive)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<string>> GetUserRoleNamesAsync(Guid userId, CancellationToken cancellationToken = default)
        => await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .Join(_context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r.Name)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<string>> GetUserPermissionsAsync(Guid userId, CancellationToken cancellationToken = default)
        => await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .Join(_context.RolePermissions, ur => ur.RoleId, rp => rp.RoleId, (ur, rp) => rp)
            .Select(rp => rp.Permission)
            .Distinct()
            .ToListAsync(cancellationToken);

    public async Task<string?> GetUserPasswordHashAsync(Guid userId, CancellationToken cancellationToken = default)
        => await _context.ApplicationUsers
            .Where(u => u.Id == userId)
            .Select(u => u.PasswordHash)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task SetUserPasswordHashAsync(Guid userId, string hashedPassword, CancellationToken cancellationToken = default)
    {
        var user = await _context.ApplicationUsers.FindAsync(new object[] { userId }, cancellationToken);
        if (user is not null)
        {
            user.SetPasswordHash(hashedPassword);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public void AddRole(Role role) => _context.Roles.Add(role);
    public void AddRolePermission(RolePermission rolePermission) => _context.RolePermissions.Add(rolePermission);
    public void RemoveRolePermission(RolePermission rolePermission) => _context.RolePermissions.Remove(rolePermission);
    public void UpdateRole(Role role) => _context.Roles.Update(role);
    public void RemoveRole(Role role) => _context.Roles.Remove(role);
    public void AddUser(ApplicationUser user) => _context.ApplicationUsers.Add(user);
    public void AddRefreshToken(RefreshToken refreshToken) => _context.RefreshTokens.Add(refreshToken);
    public void AddUserSession(UserSession session) => _context.UserSessions.Add(session);
    public void AddAuditLog(AuditLog auditLog) => _context.AuthAuditLogs.Add(auditLog);
    public void AddUserRole(UserRole userRole) => _context.UserRoles.Add(userRole);
    public void RemoveUserRole(UserRole userRole) => _context.UserRoles.Remove(userRole);
    public void UpdateUser(ApplicationUser user) => _context.ApplicationUsers.Update(user);
    public void UpdateRefreshToken(RefreshToken refreshToken) => _context.RefreshTokens.Update(refreshToken);
    public void UpdateSession(UserSession session) => _context.UserSessions.Update(session);
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => _context.SaveChangesAsync(cancellationToken);
}
