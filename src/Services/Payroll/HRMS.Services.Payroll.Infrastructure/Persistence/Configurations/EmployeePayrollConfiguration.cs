using HRMS.Services.Payroll.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Payroll.Infrastructure.Persistence.Configurations;

public class EmployeePayrollConfiguration : IEntityTypeConfiguration<EmployeePayroll>
{
    public void Configure(EntityTypeBuilder<EmployeePayroll> builder)
    {
        builder.ToTable("EmployeePayrolls");

        builder.HasKey(ep => ep.Id);

        builder.Property(ep => ep.PayrollRunId).IsRequired();
        builder.Property(ep => ep.EmployeeId).IsRequired();
        builder.Property(ep => ep.DepartmentId).IsRequired();
        builder.Property(ep => ep.DesignationId).IsRequired();
        builder.Property(ep => ep.BasicSalary).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(ep => ep.GrossSalary).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(ep => ep.TotalEarnings).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(ep => ep.TotalDeductions).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(ep => ep.NetPayable).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(ep => ep.AttendanceDays).HasDefaultValue(0);
        builder.Property(ep => ep.LOPDays).HasDefaultValue(0);
        builder.Property(ep => ep.WorkingDays).HasDefaultValue(0);
        builder.Property(ep => ep.PaidDays).HasDefaultValue(0);
        builder.Property(ep => ep.OvertimeHours).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(ep => ep.OvertimeAmount).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(ep => ep.IsProcessed).HasDefaultValue(false);
        builder.Property(ep => ep.TenantId).IsRequired();

        builder.HasMany(ep => ep.Allowances)
            .WithOne(a => a.EmployeePayroll)
            .HasForeignKey(a => a.EmployeePayrollId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(ep => ep.Deductions)
            .WithOne(d => d.EmployeePayroll)
            .HasForeignKey(d => d.EmployeePayrollId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(ep => ep.LoanRepayments)
            .WithOne(lr => lr.EmployeePayroll)
            .HasForeignKey(lr => lr.EmployeePayrollId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(ep => new { ep.EmployeeId, ep.PayrollRunId });
    }
}
