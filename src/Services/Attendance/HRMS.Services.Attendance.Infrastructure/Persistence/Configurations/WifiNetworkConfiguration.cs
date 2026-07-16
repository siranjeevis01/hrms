using HRMS.Services.Attendance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Attendance.Infrastructure.Persistence.Configurations;

public class WifiNetworkConfiguration : IEntityTypeConfiguration<WifiNetwork>
{
    public void Configure(EntityTypeBuilder<WifiNetwork> builder)
    {
        builder.ToTable("WifiNetworks");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.CompanyId)
            .IsRequired();

        builder.Property(w => w.BranchId);

        builder.Property(w => w.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(w => w.Ssid)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(w => w.Bssid)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(w => w.IsPrimary)
            .IsRequired();

        builder.Property(w => w.IsActive)
            .IsRequired();

        builder.Property(w => w.TenantId)
            .IsRequired();

        builder.HasIndex(w => w.CompanyId);
        builder.HasIndex(w => w.Ssid);
        builder.HasIndex(w => w.IsActive);
        builder.HasIndex(w => w.TenantId);
    }
}
