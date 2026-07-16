using HRMS.Services.Identity.Domain.Entities;
using HRMS.Services.Identity.Infrastructure.Repositories.Interfaces;
using HRMS.Shared.Kernel.Common;
using HRMS.Shared.Kernel.Enums;
using HRMS.Services.Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Identity.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    protected readonly IdentityDbContext Context;
    protected readonly DbSet<ApplicationUser> DbSet;

    public UserRepository(IdentityDbContext context)
    {
        Context = context;
        DbSet = context.Set<ApplicationUser>();
    }

    public virtual async Task<ApplicationUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public virtual async Task<ApplicationUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .FirstOrDefaultAsync(
                e => e.Email == email.ToLowerInvariant().Trim(),
                cancellationToken);
    }

    public virtual async Task<ApplicationUser?> GetByFirebaseUidAsync(string firebaseUid, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .FirstOrDefaultAsync(
                e => e.FirebaseUid == firebaseUid,
                cancellationToken);
    }

    public virtual async Task<ApplicationUser?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .FirstOrDefaultAsync(
                e => e.PhoneNumber == phoneNumber.Trim(),
                cancellationToken);
    }

    public virtual async Task<PagedResult<ApplicationUser>> GetUsersByTenantAsync(
        Guid tenantId,
        PaginationRequest pagination,
        CancellationToken cancellationToken = default)
    {
        IQueryable<ApplicationUser> query = DbSet
            .AsNoTracking()
            .Where(e => e.TenantId == tenantId);

        if (!string.IsNullOrWhiteSpace(pagination.SearchTerm))
        {
            var search = pagination.SearchTerm.ToLower().Trim();
            query = query.Where(e =>
                e.Email.Contains(search) ||
                e.FirstName.Contains(search) ||
                e.LastName.Contains(search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        query = pagination.SortBy?.ToLower() switch
        {
            "email" => pagination.SortOrder == SortOrder.Descending
                ? query.OrderByDescending(e => e.Email)
                : query.OrderBy(e => e.Email),
            "firstname" => pagination.SortOrder == SortOrder.Descending
                ? query.OrderByDescending(e => e.FirstName)
                : query.OrderBy(e => e.FirstName),
            "lastname" => pagination.SortOrder == SortOrder.Descending
                ? query.OrderByDescending(e => e.LastName)
                : query.OrderBy(e => e.LastName),
            _ => query.OrderByDescending(e => e.CreatedAt)
        };

        var items = await query
            .Skip(pagination.Skip)
            .Take(pagination.PageSize)
            .ToListAsync(cancellationToken);

        return PagedResult<ApplicationUser>.Create(items, totalCount, pagination.PageNumber, pagination.PageSize);
    }

    public virtual async Task<IReadOnlyList<ApplicationUser>> GetUsersByRoleAsync(
        string roleName,
        Guid? tenantId = null,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(u => u.UserRoles.Any(ur => ur.Role!.Name == roleName))
            .Where(u => tenantId == null || u.TenantId == tenantId)
            .OrderBy(u => u.Email)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(
            e => e.Email == email.ToLowerInvariant().Trim(),
            cancellationToken);
    }

    public virtual async Task<bool> ExistsByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(
            e => e.PhoneNumber == phoneNumber.Trim(),
            cancellationToken);
    }

    public virtual async Task<ApplicationUser?> GetUserWithRolesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public virtual async Task<ApplicationUser?> GetUserWithPermissionsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(u => u.UserPermissions)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public virtual async Task<ApplicationUser?> GetUserWithSessionsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Context.Users
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                    .ThenInclude(r => r!.Permissions)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public virtual async Task<ApplicationUser> AddAsync(ApplicationUser user, CancellationToken cancellationToken = default)
    {
        var entry = await DbSet.AddAsync(user, cancellationToken);
        return entry.Entity;
    }

    public virtual Task UpdateAsync(ApplicationUser user, CancellationToken cancellationToken = default)
    {
        DbSet.Update(user);
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(ApplicationUser user, CancellationToken cancellationToken = default)
    {
        user.Deactivate();
        DbSet.Update(user);
        return Task.CompletedTask;
    }

    public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.CountAsync(cancellationToken);
    }

    public virtual IQueryable<ApplicationUser> Query()
    {
        return DbSet.AsQueryable();
    }
}
