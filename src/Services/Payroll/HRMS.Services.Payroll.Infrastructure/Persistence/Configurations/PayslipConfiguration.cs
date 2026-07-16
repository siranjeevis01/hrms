using HRMS.Services.Payroll.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Payroll.Infrastructure.Persistence.Configurations;

public class PayslipConfiguration : IEntityTypeConfiguration<Payslip>
{
    public void Configure(EntityTypeBuilder<Payslip> builder)
    {
        builder.ToTable("Payslips");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.EmployeePayrollId).IsRequired();
        builder.Property(p => p.EmployeeId).IsRequired();
        builder.Property(p => p.Month).IsRequired();
        builder.Property(p => p.Year).IsRequired();
        builder.Property(p => p.PdfUrl).HasMaxLength(500);
        builder.Property(p => p.GeneratedAt).IsRequired();
        builder.Property(p => p.IsViewed).HasDefaultValue(false);
        builder.Property(p => p.TenantId).IsRequired();

        builder.HasOne(p => p.EmployeePayroll)
            .WithMany()
            .HasForeignKey(p => p.EmployeePayrollId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(p => new { p.EmployeeId, p.Month, p.Year });
    }
}
