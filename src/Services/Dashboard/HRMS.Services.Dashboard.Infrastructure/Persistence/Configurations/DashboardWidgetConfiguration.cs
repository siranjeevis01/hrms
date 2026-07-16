using HRMS.Services.Dashboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Dashboard.Infrastructure.Persistence.Configurations;

public class DashboardWidgetConfiguration : IEntityTypeConfiguration<DashboardWidget>
{
    public void Configure(EntityTypeBuilder<DashboardWidget> builder)
    {
        builder.ToTable("DashboardWidgets");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(w => w.DataSource)
            .HasMaxLength(500);

        builder.Property(w => w.Configuration)
            .HasMaxLength(4000);

        builder.Property(w => w.Position)
            .HasMaxLength(500);

        builder.Property(w => w.Size)
            .HasMaxLength(500);

        builder.Property(w => w.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(w => w.DashboardId);
        builder.HasIndex(w => w.WidgetType);
        builder.HasIndex(w => w.TenantId);
    }
}
