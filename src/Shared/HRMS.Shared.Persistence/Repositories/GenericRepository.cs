using System.Linq.Expressions;
using HRMS.Shared.Kernel.Common;
using HRMS.Shared.Kernel.Enums;
using HRMS.Shared.Kernel.Interfaces;
using HRMS.Shared.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Shared.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext Context;
    protected readonly DbSet<T> DbSet;
    private readonly ITenantService _tenantService;

    public GenericRepository(ApplicationDbContext context, ITenantService tenantService)
    {
        Context = context;
        DbSet = context.Set<T>();
        _tenantService = tenantService;
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public virtual async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<IReadOnlyList<T>> FindAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        var currentTenant = _tenantService.GetCurrentTenant();
        if (currentTenant is not null && entity.TenantId == Guid.Empty)
        {
            entity.TenantId = currentTenant.Id;
        }

        var entry = await DbSet.AddAsync(entity, cancellationToken);
        return entry.Entity;
    }

    public virtual Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbSet.Update(entity);
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.MarkAsDeleted();
        DbSet.Update(entity);
        return Task.CompletedTask;
    }

    public virtual async Task<PagedResult<T>> GetPagedAsync(
        PaginationRequest request,
        Expression<Func<T, bool>>? filter = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = DbSet.AsNoTracking();

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        if (!string.IsNullOrWhiteSpace(request.SortBy))
        {
            query = request.SortBy.ToLower() switch
            {
                "createdat" => request.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(e => e.CreatedAt)
                    : query.OrderBy(e => e.CreatedAt),
                "updatedat" => request.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(e => e.UpdatedAt)
                    : query.OrderBy(e => e.UpdatedAt),
                "status" => request.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(e => e.Status)
                    : query.OrderBy(e => e.Status),
                _ => query.OrderBy(e => e.CreatedAt)
            };
        }
        else
        {
            query = query.OrderByDescending(e => e.CreatedAt);
        }

        var items = await query
            .Skip(request.Skip)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return PagedResult<T>.Create(items, totalCount, request.PageNumber, request.PageSize);
    }

    public virtual async Task<bool> ExistsAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(predicate, cancellationToken);
    }

    public virtual async Task<int> CountAsync(
        Expression<Func<T, bool>>? filter = null,
        CancellationToken cancellationToken = default)
    {
        return filter is null
            ? await DbSet.CountAsync(cancellationToken)
            : await DbSet.CountAsync(filter, cancellationToken);
    }

    public virtual async Task<IReadOnlyList<T>> GetByTenantAsync(
        Guid tenantId,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(e => e.TenantId == tenantId)
            .ToListAsync(cancellationToken);
    }

    public virtual IQueryable<T> Query()
    {
        return DbSet.AsQueryable();
    }
}
