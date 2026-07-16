using Microsoft.EntityFrameworkCore;
using HRMS.Services.Organization.Application.Interfaces;
using HRMS.Services.Organization.Domain.Entities;
using HRMS.Services.Organization.Infrastructure.Persistence;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Organization.Infrastructure.Repositories;

public interface IOrganizationRepository<T> where T : AggregateRoot
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> FindAsync(Func<T, bool> predicate, CancellationToken cancellationToken = default);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Func<T, bool>? predicate = null, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}

public class OrganizationRepository<T> : IOrganizationRepository<T> where T : AggregateRoot
{
    private readonly OrganizationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public OrganizationRepository(OrganizationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public virtual async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<IReadOnlyList<T>> FindAsync(Func<T, bool> predicate, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(
            _dbSet.AsNoTracking().Where(predicate).ToList());
    }

    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<int> CountAsync(Func<T, bool>? predicate = null, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(
            predicate == null
                ? _dbSet.Count()
                : _dbSet.Count(predicate));
    }

    public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(e => e.Id == id, cancellationToken);
    }
}

public class CompanyRepository : OrganizationRepository<Company>
{
    private readonly OrganizationDbContext _context;

    public CompanyRepository(OrganizationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Company?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Companies
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Name == name, cancellationToken);
    }

    public async Task<Company?> GetByRegistrationNumberAsync(string registrationNumber, CancellationToken cancellationToken = default)
    {
        return await _context.Companies
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.RegistrationNumber == registrationNumber, cancellationToken);
    }
}

public class BranchRepository : OrganizationRepository<Branch>
{
    private readonly OrganizationDbContext _context;

    public BranchRepository(OrganizationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Branch>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
    {
        return await _context.Branches
            .AsNoTracking()
            .Where(b => b.CompanyId == companyId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Branch?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _context.Branches
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Code == code, cancellationToken);
    }
}

public class DepartmentRepository : OrganizationRepository<Department>
{
    private readonly OrganizationDbContext _context;

    public DepartmentRepository(OrganizationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Department>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
    {
        return await _context.Departments
            .AsNoTracking()
            .Where(d => d.CompanyId == companyId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Department>> GetByParentIdAsync(Guid? parentId, CancellationToken cancellationToken = default)
    {
        return await _context.Departments
            .AsNoTracking()
            .Where(d => d.ParentDepartmentId == parentId)
            .ToListAsync(cancellationToken);
    }
}
