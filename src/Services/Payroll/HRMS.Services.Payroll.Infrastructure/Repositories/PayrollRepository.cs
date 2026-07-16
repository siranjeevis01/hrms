using HRMS.Services.Payroll.Application.Interfaces;
using HRMS.Services.Payroll.Domain.Entities;
using HRMS.Services.Payroll.Domain.Enums;
using HRMS.Services.Payroll.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Infrastructure.Repositories;

public class PayrollRepository : IPayrollRepository
{
    private readonly IPayrollDbContext _context;

    public PayrollRepository(IPayrollDbContext context)
    {
        _context = context;
    }

    public async Task<PayrollRun?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.PayrollRuns
            .Include(r => r.EmployeePayrolls)
                .ThenInclude(ep => ep.Allowances)
            .Include(r => r.EmployeePayrolls)
                .ThenInclude(ep => ep.Deductions)
            .Include(r => r.EmployeePayrolls)
                .ThenInclude(ep => ep.LoanRepayments)
            .Include(r => r.AuditLogs)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<PayrollRun?> GetByMonthYearAsync(Guid companyId, int month, int year, CancellationToken cancellationToken = default)
    {
        return await _context.PayrollRuns
            .FirstOrDefaultAsync(r => r.CompanyId == companyId && r.Month == month
                && r.Year == year && r.Status != PayrollStatus.Reversed, cancellationToken);
    }

    public async Task<List<PayrollRun>> GetPayrollRunsAsync(Guid companyId, int? month, int? year, CancellationToken cancellationToken = default)
    {
        var query = _context.PayrollRuns.Where(r => r.CompanyId == companyId);

        if (month.HasValue)
            query = query.Where(r => r.Month == month.Value);
        if (year.HasValue)
            query = query.Where(r => r.Year == year.Value);

        return await query.OrderByDescending(r => r.Year).ThenByDescending(r => r.Month)
            .ToListAsync(cancellationToken);
    }

    public async Task<EmployeePayroll?> GetEmployeePayrollAsync(Guid employeeId, int month, int year, CancellationToken cancellationToken = default)
    {
        return await _context.EmployeePayrolls
            .Include(ep => ep.Allowances)
            .Include(ep => ep.Deductions)
            .FirstOrDefaultAsync(ep => ep.EmployeeId == employeeId
                && ep.PayrollRun.Month == month && ep.PayrollRun.Year == year, cancellationToken);
    }

    public async Task<List<EmployeePayroll>> GetEmployeePayrollHistoryAsync(Guid employeeId, CancellationToken cancellationToken = default)
    {
        return await _context.EmployeePayrolls
            .Include(ep => ep.Allowances)
            .Include(ep => ep.Deductions)
            .Where(ep => ep.EmployeeId == employeeId)
            .OrderByDescending(ep => ep.PayrollRun.Year)
            .ThenByDescending(ep => ep.PayrollRun.Month)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
    {
        if (entity is PayrollRun run)
            await _context.PayrollRuns.AddAsync(run, cancellationToken);
        else if (entity is EmployeePayroll ep)
            await _context.EmployeePayrolls.AddAsync(ep, cancellationToken);
        else if (entity is Bonus bonus)
            await _context.Bonuses.AddAsync(bonus, cancellationToken);
        else if (entity is Loan loan)
            await _context.Loans.AddAsync(loan, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
