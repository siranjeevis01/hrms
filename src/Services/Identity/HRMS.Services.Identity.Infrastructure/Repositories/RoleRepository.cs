using HRMS.Services.Identity.Domain.Entities;
using HRMS.Services.Identity.Infrastructure.Repositories.Interfaces;
using HRMS.Services.Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Identity.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    protected readonly IdentityDbContext Context;
    protected readonly DbSet<Role> DbSet;

    public RoleRepository(IdentityDbContext context)
    {
        Context = context;
        DbSet = context.Set<Role>();
    }

    public virtual async Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public virtual async Task<IReadOnlyList<Role>> GetAllRolesAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .OrderBy(r => r.Name)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .FirstOrDefaultAsync(
                r => r.Name == name.Trim(),
                cancellationToken);
    }

    public virtual async Task<Role?> GetRoleWithPermissionsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(r => r.Permissions)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public virtual async Task<IReadOnlyList<Role>> GetRolesByTenantAsync(
        Guid tenantId,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(r => r.TenantId == tenantId)
            .OrderBy(r => r.Name)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<Role> AddAsync(Role role, CancellationToken cancellationToken = default)
    {
        var entry = await DbSet.AddAsync(role, cancellationToken);
        return entry.Entity;
    }

    public virtual Task UpdateAsync(Role role, CancellationToken cancellationToken = default)
    {
        DbSet.Update(role);
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(Role role, CancellationToken cancellationToken = default)
    {
        DbSet.Remove(role);
        return Task.CompletedTask;
    }

    public virtual async Task<bool> ExistsByNameAsync(
        string name,
        Guid? tenantId = null,
        CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(
            r => r.Name == name.Trim() &&
                 (tenantId == null || r.TenantId == tenantId),
            cancellationToken);
    }

    public virtual IQueryable<Role> Query()
    {
        return DbSet.AsQueryable();
    }
}
