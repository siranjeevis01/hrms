using HRMS.Services.Payroll.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Payroll.Infrastructure.Persistence.Configurations;

public class PayrollRunConfiguration : IEntityTypeConfiguration<PayrollRun>
{
    public void Configure(EntityTypeBuilder<PayrollRun> builder)
    {
        builder.ToTable("PayrollRuns");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.CompanyId).IsRequired();
        builder.Property(r => r.Month).IsRequired();
        builder.Property(r => r.Year).IsRequired();
        builder.Property(r => r.Status).IsRequired().HasConversion<int>();
        builder.Property(r => r.TotalEmployees).HasDefaultValue(0);
        builder.Property(r => r.TotalGrossAmount).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(r => r.TotalDeductions).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(r => r.TotalNetAmount).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(r => r.TenantId).IsRequired();

        builder.HasMany(r => r.EmployeePayrolls)
            .WithOne(ep => ep.PayrollRun)
            .HasForeignKey(ep => ep.PayrollRunId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.AuditLogs)
            .WithOne(al => al.PayrollRun)
            .HasForeignKey(al => al.PayrollRunId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(r => new { r.CompanyId, r.Month, r.Year });
    }
}
