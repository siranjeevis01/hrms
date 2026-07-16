using HRMS.Services.Audit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Audit.Infrastructure.Persistence.Configurations;

public class DataChangeLogConfiguration : IEntityTypeConfiguration<DataChangeLog>
{
    public void Configure(EntityTypeBuilder<DataChangeLog> builder)
    {
        builder.ToTable("DataChangeLogs");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.UserId)
            .IsRequired();

        builder.Property(d => d.EntityType)
            .IsRequired();

        builder.Property(d => d.EntityId)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(d => d.ChangeType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(d => d.SerializedData)
            .HasMaxLength(4000);

        builder.Property(d => d.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(d => d.UserId);
        builder.HasIndex(d => d.EntityType);
        builder.HasIndex(d => d.Timestamp);
        builder.HasIndex(d => d.TenantId);
    }
}
