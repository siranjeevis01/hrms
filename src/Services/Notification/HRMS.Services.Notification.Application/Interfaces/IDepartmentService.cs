namespace HRMS.Services.Notification.Application.Interfaces;

public interface IDepartmentService
{
    Task<List<Guid>> GetDepartmentUserIdsAsync(Guid departmentId, CancellationToken cancellationToken = default);
}
