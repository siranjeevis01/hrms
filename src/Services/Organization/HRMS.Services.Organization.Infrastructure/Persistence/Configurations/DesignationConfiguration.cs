using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HRMS.Services.Organization.Domain.Entities;

namespace HRMS.Services.Organization.Infrastructure.Persistence.Configurations;

public class DesignationConfiguration : IEntityTypeConfiguration<Designation>
{
    public void Configure(EntityTypeBuilder<Designation> builder)
    {
        builder.ToTable("Designations");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(d => d.Description)
            .HasMaxLength(1000);

        builder.Property(d => d.MinSalary)
            .HasColumnType("decimal(18,2)");

        builder.Property(d => d.MaxSalary)
            .HasColumnType("decimal(18,2)");

        builder.Property(d => d.CompanyId)
            .IsRequired();

        builder.HasIndex(d => d.CompanyId);
        builder.HasIndex(d => d.Code);
        builder.HasIndex(d => d.TenantId);
    }
}
