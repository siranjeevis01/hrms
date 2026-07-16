using HRMS.Services.Dashboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Dashboard.Infrastructure.Persistence.Configurations;

public class DashboardConfiguration : IEntityTypeConfiguration<HRMS.Services.Dashboard.Domain.Entities.Dashboard>
{
    public void Configure(EntityTypeBuilder<HRMS.Services.Dashboard.Domain.Entities.Dashboard> builder)
    {
        builder.ToTable("Dashboards");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.Description)
            .HasMaxLength(1000);

        builder.Property(d => d.Layout)
            .HasMaxLength(4000);

        builder.Property(d => d.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(d => d.UserId);
        builder.HasIndex(d => d.IsDefault);
        builder.HasIndex(d => d.TenantId);

        builder.HasMany(d => d.Widgets)
            .WithOne()
            .HasForeignKey(w => w.DashboardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Shares)
            .WithOne()
            .HasForeignKey(s => s.DashboardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(d => d.DomainEvents);
    }
}
