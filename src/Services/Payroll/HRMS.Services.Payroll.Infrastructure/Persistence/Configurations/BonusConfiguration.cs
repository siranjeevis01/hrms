using HRMS.Services.Payroll.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Payroll.Infrastructure.Persistence.Configurations;

public class BonusConfiguration : IEntityTypeConfiguration<Bonus>
{
    public void Configure(EntityTypeBuilder<Bonus> builder)
    {
        builder.ToTable("Bonuses");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.EmployeeId).IsRequired();
        builder.Property(b => b.BonusType).IsRequired().HasConversion<int>();
        builder.Property(b => b.Amount).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(b => b.Month).IsRequired();
        builder.Property(b => b.Year).IsRequired();
        builder.Property(b => b.Status).IsRequired().HasConversion<int>();
        builder.Property(b => b.TenantId).IsRequired();

        builder.HasIndex(b => new { b.EmployeeId, b.Month, b.Year });
    }
}
