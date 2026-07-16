using HRMS.Services.Audit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Audit.Infrastructure.Persistence.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLogs");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.UserName)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(a => a.ActionType)
            .IsRequired();

        builder.Property(a => a.EntityType)
            .IsRequired();

        builder.Property(a => a.EntityId)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(a => a.OldValues)
            .HasMaxLength(4000);

        builder.Property(a => a.NewValues)
            .HasMaxLength(4000);

        builder.Property(a => a.IpAddress)
            .HasMaxLength(45);

        builder.Property(a => a.UserAgent)
            .HasMaxLength(500);

        builder.Property(a => a.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(a => a.UserId);
        builder.HasIndex(a => a.EntityType);
        builder.HasIndex(a => a.ActionType);
        builder.HasIndex(a => a.Timestamp);
        builder.HasIndex(a => a.TenantId);

        builder.HasMany(a => a.AuditTrails)
            .WithOne()
            .HasForeignKey(t => t.AuditLogId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(a => a.DomainEvents);
    }
}
