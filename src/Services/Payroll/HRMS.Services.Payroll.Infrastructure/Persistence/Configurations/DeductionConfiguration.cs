using HRMS.Services.Payroll.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Payroll.Infrastructure.Persistence.Configurations;

public class DeductionConfiguration : IEntityTypeConfiguration<Deduction>
{
    public void Configure(EntityTypeBuilder<Deduction> builder)
    {
        builder.ToTable("Deductions");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.EmployeePayrollId).IsRequired();
        builder.Property(d => d.ComponentDefId).IsRequired();
        builder.Property(d => d.Name).IsRequired().HasMaxLength(100);
        builder.Property(d => d.Amount).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(d => d.Type).IsRequired().HasConversion<int>();
        builder.Property(d => d.IsStatutory).HasDefaultValue(false);
    }
}
