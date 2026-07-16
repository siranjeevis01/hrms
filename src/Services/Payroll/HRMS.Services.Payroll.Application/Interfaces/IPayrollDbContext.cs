using HRMS.Services.Payroll.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Interfaces;

public interface IPayrollDbContext
{
    DbSet<PayrollRun> PayrollRuns { get; }
    DbSet<EmployeePayroll> EmployeePayrolls { get; }
    DbSet<SalaryComponentDef> SalaryComponentDefs { get; }
    DbSet<EmployeeSalaryAssignment> EmployeeSalaryAssignments { get; }
    DbSet<Allowance> Allowances { get; }
    DbSet<Deduction> Deductions { get; }
    DbSet<Bonus> Bonuses { get; }
    DbSet<Loan> Loans { get; }
    DbSet<LoanRepayment> LoanRepayments { get; }
    DbSet<TaxConfiguration> TaxConfigurations { get; }
    DbSet<EmployeeTaxDeclaration> EmployeeTaxDeclarations { get; }
    DbSet<Payslip> Payslips { get; }
    DbSet<PayrollAuditLog> PayrollAuditLogs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
