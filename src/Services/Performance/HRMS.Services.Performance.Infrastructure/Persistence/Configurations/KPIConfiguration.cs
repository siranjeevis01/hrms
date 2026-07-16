using HRMS.Services.Performance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Performance.Infrastructure.Persistence.Configurations;

public class KPIConfiguration : IEntityTypeConfiguration<KPI>
{
    public void Configure(EntityTypeBuilder<KPI> builder)
    {
        builder.ToTable("KPIs");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .HasMaxLength(1000);

        builder.Property(e => e.Unit)
            .HasMaxLength(50);

        builder.Property(e => e.Period)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.EmployeeId);
        builder.HasIndex(e => e.DepartmentId);
        builder.HasIndex(e => e.Category);
        builder.HasIndex(e => e.Period);
        builder.HasIndex(e => e.TenantId);

        builder.Ignore(e => e.DomainEvents);
    }
}
