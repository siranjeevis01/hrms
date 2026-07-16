using HRMS.Services.Dashboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Dashboard.Infrastructure.Persistence.Configurations;

public class DashboardShareConfiguration : IEntityTypeConfiguration<DashboardShare>
{
    public void Configure(EntityTypeBuilder<DashboardShare> builder)
    {
        builder.ToTable("DashboardShares");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Permission)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(s => s.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(s => s.DashboardId);
        builder.HasIndex(s => s.SharedWithUserId);
        builder.HasIndex(s => s.TenantId);
    }
}
