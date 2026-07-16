using HRMS.Services.Payroll.Application.Interfaces;
using HRMS.Services.Payroll.Domain.Entities;
using HRMS.Services.Payroll.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Infrastructure.Persistence;

public class PayrollDbContext : DbContext, IPayrollDbContext
{
    public PayrollDbContext(DbContextOptions<PayrollDbContext> options) : base(options) { }

    public DbSet<PayrollRun> PayrollRuns => Set<PayrollRun>();
    public DbSet<EmployeePayroll> EmployeePayrolls => Set<EmployeePayroll>();
    public DbSet<SalaryComponentDef> SalaryComponentDefs => Set<SalaryComponentDef>();
    public DbSet<EmployeeSalaryAssignment> EmployeeSalaryAssignments => Set<EmployeeSalaryAssignment>();
    public DbSet<Allowance> Allowances => Set<Allowance>();
    public DbSet<Deduction> Deductions => Set<Deduction>();
    public DbSet<Bonus> Bonuses => Set<Bonus>();
    public DbSet<Loan> Loans => Set<Loan>();
    public DbSet<LoanRepayment> LoanRepayments => Set<LoanRepayment>();
    public DbSet<TaxConfiguration> TaxConfigurations => Set<TaxConfiguration>();
    public DbSet<EmployeeTaxDeclaration> EmployeeTaxDeclarations => Set<EmployeeTaxDeclaration>();
    public DbSet<Payslip> Payslips => Set<Payslip>();
    public DbSet<PayrollAuditLog> PayrollAuditLogs => Set<PayrollAuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PayrollRunConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeePayrollConfiguration());
        modelBuilder.ApplyConfiguration(new SalaryComponentDefConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeSalaryAssignmentConfiguration());
        modelBuilder.ApplyConfiguration(new AllowanceConfiguration());
        modelBuilder.ApplyConfiguration(new DeductionConfiguration());
        modelBuilder.ApplyConfiguration(new BonusConfiguration());
        modelBuilder.ApplyConfiguration(new LoanConfiguration());
        modelBuilder.ApplyConfiguration(new LoanRepaymentConfiguration());
        modelBuilder.ApplyConfiguration(new TaxConfigurationConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeTaxDeclarationConfiguration());
        modelBuilder.ApplyConfiguration(new PayslipConfiguration());
        modelBuilder.ApplyConfiguration(new PayrollAuditLogConfiguration());
    }
}
