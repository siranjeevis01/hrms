using HRMS.Services.Payroll.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Payroll.Infrastructure.Persistence.Configurations;

public class SalaryComponentDefConfiguration : IEntityTypeConfiguration<SalaryComponentDef>
{
    public void Configure(EntityTypeBuilder<SalaryComponentDef> builder)
    {
        builder.ToTable("SalaryComponentDefs");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Code).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Type).IsRequired().HasConversion<int>();
        builder.Property(c => c.CalculationType).IsRequired().HasConversion<int>();
        builder.Property(c => c.DefaultValue).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(c => c.Formula).HasMaxLength(500);
        builder.Property(c => c.IsTaxable).HasDefaultValue(true);
        builder.Property(c => c.IsPensionable).HasDefaultValue(false);
        builder.Property(c => c.IsPFApplicable).HasDefaultValue(false);
        builder.Property(c => c.IsESIApplicable).HasDefaultValue(false);
        builder.Property(c => c.IsActive).HasDefaultValue(true);
        builder.Property(c => c.SortOrder).HasDefaultValue(0);
        builder.Property(c => c.TenantId).IsRequired();

        builder.HasIndex(c => c.Code).IsUnique();
    }
}
