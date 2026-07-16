using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HRMS.Services.Organization.Domain.Entities;

namespace HRMS.Services.Organization.Infrastructure.Persistence.Configurations;

public class CompanyPolicyConfiguration : IEntityTypeConfiguration<CompanyPolicy>
{
    public void Configure(EntityTypeBuilder<CompanyPolicy> builder)
    {
        builder.ToTable("CompanyPolicies");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .HasMaxLength(1000);

        builder.Property(p => p.Content)
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        builder.Property(p => p.CompanyId)
            .IsRequired();

        builder.HasIndex(p => p.CompanyId);
        builder.HasIndex(p => p.Category);
        builder.HasIndex(p => p.TenantId);
    }
}
