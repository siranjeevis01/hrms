using HRMS.Services.Dashboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Dashboard.Infrastructure.Persistence.Configurations;

public class WidgetPresetConfiguration : IEntityTypeConfiguration<WidgetPreset>
{
    public void Configure(EntityTypeBuilder<WidgetPreset> builder)
    {
        builder.ToTable("WidgetPresets");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.DefaultConfiguration)
            .HasMaxLength(4000);

        builder.Property(p => p.Description)
            .HasMaxLength(1000);

        builder.Property(p => p.Category)
            .HasMaxLength(100);

        builder.Property(p => p.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(p => p.WidgetType);
        builder.HasIndex(p => p.TenantId);
    }
}
