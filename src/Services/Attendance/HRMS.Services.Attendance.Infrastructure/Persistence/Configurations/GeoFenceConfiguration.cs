using HRMS.Services.Attendance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Attendance.Infrastructure.Persistence.Configurations;

public class GeoFenceConfiguration : IEntityTypeConfiguration<GeoFence>
{
    public void Configure(EntityTypeBuilder<GeoFence> builder)
    {
        builder.ToTable("GeoFences");

        builder.HasKey(g => g.Id);

        builder.Property(g => g.CompanyId)
            .IsRequired();

        builder.Property(g => g.BranchId);

        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(g => g.Latitude)
            .IsRequired();

        builder.Property(g => g.Longitude)
            .IsRequired();

        builder.Property(g => g.RadiusMeters)
            .IsRequired();

        builder.Property(g => g.IsActive)
            .IsRequired();

        builder.Property(g => g.WorkingDays)
            .HasMaxLength(200);

        builder.Property(g => g.TenantId)
            .IsRequired();

        builder.HasIndex(g => g.CompanyId);
        builder.HasIndex(g => g.IsActive);
        builder.HasIndex(g => g.TenantId);
    }
}
