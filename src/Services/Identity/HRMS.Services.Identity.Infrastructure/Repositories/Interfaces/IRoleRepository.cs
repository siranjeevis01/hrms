using HRMS.Services.Identity.Domain.Entities;

namespace HRMS.Services.Identity.Infrastructure.Repositories.Interfaces;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Role>> GetAllRolesAsync(CancellationToken cancellationToken = default);
    Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<Role?> GetRoleWithPermissionsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Role>> GetRolesByTenantAsync(Guid tenantId, CancellationToken cancellationToken = default);
    Task<Role> AddAsync(Role role, CancellationToken cancellationToken = default);
    Task UpdateAsync(Role role, CancellationToken cancellationToken = default);
    Task DeleteAsync(Role role, CancellationToken cancellationToken = default);
    Task<bool> ExistsByNameAsync(string name, Guid? tenantId = null, CancellationToken cancellationToken = default);
    IQueryable<Role> Query();
}
