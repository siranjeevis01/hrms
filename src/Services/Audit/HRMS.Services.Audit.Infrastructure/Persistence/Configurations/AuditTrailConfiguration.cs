using HRMS.Services.Audit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Audit.Infrastructure.Persistence.Configurations;

public class AuditTrailConfiguration : IEntityTypeConfiguration<AuditTrail>
{
    public void Configure(EntityTypeBuilder<AuditTrail> builder)
    {
        builder.ToTable("AuditTrails");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.AuditLogId)
            .IsRequired();

        builder.Property(t => t.FieldName)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(t => t.OldValue)
            .HasMaxLength(4000);

        builder.Property(t => t.NewValue)
            .HasMaxLength(4000);

        builder.Property(t => t.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(t => t.AuditLogId);
        builder.HasIndex(t => t.TenantId);
    }
}
