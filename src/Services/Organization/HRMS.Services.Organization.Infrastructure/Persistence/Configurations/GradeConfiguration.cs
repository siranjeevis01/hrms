using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HRMS.Services.Organization.Domain.Entities;

namespace HRMS.Services.Organization.Infrastructure.Persistence.Configurations;

public class GradeConfiguration : IEntityTypeConfiguration<Grade>
{
    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.ToTable("Grades");

        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(g => g.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(g => g.MinSalary)
            .HasColumnType("decimal(18,2)");

        builder.Property(g => g.MaxSalary)
            .HasColumnType("decimal(18,2)");

        builder.Property(g => g.Benefits)
            .HasMaxLength(2000);

        builder.Property(g => g.CompanyId)
            .IsRequired();

        builder.HasIndex(g => g.CompanyId);
        builder.HasIndex(g => g.Code);
        builder.HasIndex(g => g.TenantId);
    }
}
