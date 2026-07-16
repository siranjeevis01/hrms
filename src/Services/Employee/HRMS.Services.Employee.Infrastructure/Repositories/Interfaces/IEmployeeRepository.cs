using HRMS.Services.Employee.Application.DTOs;
using HRMS.Services.Employee.Domain.Enums;

namespace HRMS.Services.Employee.Infrastructure.Repositories.Interfaces;

public interface IEmployeeRepository
{
    Task<Domain.Entities.Employee?> GetByEmployeeCodeAsync(string employeeCode, CancellationToken cancellationToken = default);
    Task<Domain.Entities.Employee?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<List<Domain.Entities.Employee>> GetByDepartmentAsync(Guid departmentId, CancellationToken cancellationToken = default);
    Task<List<Domain.Entities.Employee>> GetByDesignationAsync(Guid designationId, CancellationToken cancellationToken = default);
    Task<List<Domain.Entities.Employee>> GetByStatusAsync(EmploymentStatus status, CancellationToken cancellationToken = default);
    Task<List<EmployeeListDto>> GetPagedWithFiltersAsync(
        int pageNumber, int pageSize, Guid? departmentId, Guid? designationId,
        EmploymentStatus? status, string? searchTerm, CancellationToken cancellationToken = default);
    Task<EmployeeDto?> GetEmployeeFullProfileAsync(Guid employeeId, CancellationToken cancellationToken = default);
    Task<List<EmployeeListDto>> GetNewHiresAsync(DateTime fromDate, DateTime toDate, Guid? departmentId, CancellationToken cancellationToken = default);
    Task<List<EmployeeListDto>> GetEmployeesLeavingAsync(Guid? departmentId, CancellationToken cancellationToken = default);
    Task<List<DepartmentHeadCountDto>> GetDepartmentHeadCountAsync(Guid? companyId, CancellationToken cancellationToken = default);
    Task<string> GenerateNextEmployeeCodeAsync(Guid companyId, CancellationToken cancellationToken = default);
}
