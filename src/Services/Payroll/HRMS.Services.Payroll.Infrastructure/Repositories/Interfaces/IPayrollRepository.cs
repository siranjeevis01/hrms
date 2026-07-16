using HRMS.Services.Payroll.Domain.Entities;

namespace HRMS.Services.Payroll.Infrastructure.Repositories.Interfaces;

public interface IPayrollRepository
{
    Task<PayrollRun?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PayrollRun?> GetByMonthYearAsync(Guid companyId, int month, int year, CancellationToken cancellationToken = default);
    Task<List<PayrollRun>> GetPayrollRunsAsync(Guid companyId, int? month, int? year, CancellationToken cancellationToken = default);
    Task<EmployeePayroll?> GetEmployeePayrollAsync(Guid employeeId, int month, int year, CancellationToken cancellationToken = default);
    Task<List<EmployeePayroll>> GetEmployeePayrollHistoryAsync(Guid employeeId, CancellationToken cancellationToken = default);
    Task AddAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class;
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
