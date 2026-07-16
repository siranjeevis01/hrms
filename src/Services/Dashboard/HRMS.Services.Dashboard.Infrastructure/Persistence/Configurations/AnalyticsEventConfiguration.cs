using HRMS.Services.Dashboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Dashboard.Infrastructure.Persistence.Configurations;

public class AnalyticsEventConfiguration : IEntityTypeConfiguration<AnalyticsEvent>
{
    public void Configure(EntityTypeBuilder<AnalyticsEvent> builder)
    {
        builder.ToTable("AnalyticsEvents");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.EntityId)
            .HasMaxLength(128);

        builder.Property(a => a.EntityType)
            .HasMaxLength(100);

        builder.Property(a => a.Data)
            .HasMaxLength(4000);

        builder.Property(a => a.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(a => a.EventType);
        builder.HasIndex(a => a.EmployeeId);
        builder.HasIndex(a => a.Timestamp);
        builder.HasIndex(a => a.TenantId);
    }
}
