using HRMS.Services.Payroll.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Payroll.Infrastructure.Persistence.Configurations;

public class TaxConfigurationConfiguration : IEntityTypeConfiguration<TaxConfiguration>
{
    public void Configure(EntityTypeBuilder<TaxConfiguration> builder)
    {
        builder.ToTable("TaxConfigurations");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.CompanyId).IsRequired();
        builder.Property(t => t.FinancialYear).IsRequired().HasMaxLength(20);
        builder.Property(t => t.Country).IsRequired().HasMaxLength(50).HasDefaultValue("India");
        builder.Property(t => t.TaxSlabConfig).HasColumnType("nvarchar(max)").HasDefaultValue("[]");
        builder.Property(t => t.PFContributionRate).HasColumnType("decimal(5,2)").HasDefaultValue(12);
        builder.Property(t => t.ESIContributionRate).HasColumnType("decimal(5,2)").HasDefaultValue(0.75m);
        builder.Property(t => t.ProfessionalTaxConfig).HasColumnType("nvarchar(max)").HasDefaultValue("{}");
        builder.Property(t => t.StandardDeduction).HasColumnType("decimal(18,2)").HasDefaultValue(50000);
        builder.Property(t => t.TenantId).IsRequired();

        builder.HasIndex(t => new { t.CompanyId, t.FinancialYear });
    }
}
