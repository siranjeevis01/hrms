using HRMS.Services.Identity.Domain.Entities;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Identity.Infrastructure.Repositories.Interfaces;

public interface IUserRepository
{
    Task<ApplicationUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ApplicationUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<ApplicationUser?> GetByFirebaseUidAsync(string firebaseUid, CancellationToken cancellationToken = default);
    Task<ApplicationUser?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default);
    Task<PagedResult<ApplicationUser>> GetUsersByTenantAsync(
        Guid tenantId,
        PaginationRequest pagination,
        CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ApplicationUser>> GetUsersByRoleAsync(
        string roleName,
        Guid? tenantId = null,
        CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> ExistsByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default);
    Task<ApplicationUser?> GetUserWithRolesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ApplicationUser?> GetUserWithPermissionsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ApplicationUser?> GetUserWithSessionsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ApplicationUser> AddAsync(ApplicationUser user, CancellationToken cancellationToken = default);
    Task UpdateAsync(ApplicationUser user, CancellationToken cancellationToken = default);
    Task DeleteAsync(ApplicationUser user, CancellationToken cancellationToken = default);
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    IQueryable<ApplicationUser> Query();
}
