using HRMS.Services.Payroll.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Payroll.Infrastructure.Persistence.Configurations;

public class AllowanceConfiguration : IEntityTypeConfiguration<Allowance>
{
    public void Configure(EntityTypeBuilder<Allowance> builder)
    {
        builder.ToTable("Allowances");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.EmployeePayrollId).IsRequired();
        builder.Property(a => a.ComponentDefId).IsRequired();
        builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Amount).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(a => a.Type).IsRequired().HasConversion<int>();
        builder.Property(a => a.IsTaxable).HasDefaultValue(true);
    }
}
