using HRMS.Services.Notification.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Notification.Infrastructure.Persistence.Configurations;

public class EmailQueueConfiguration : IEntityTypeConfiguration<EmailQueue>
{
    public void Configure(EntityTypeBuilder<EmailQueue> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.To).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Cc).HasMaxLength(500);
        builder.Property(e => e.Bcc).HasMaxLength(500);
        builder.Property(e => e.Subject).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Body).IsRequired();
        builder.Property(e => e.Attachments).HasMaxLength(4000);
        builder.Property(e => e.LastError).HasMaxLength(1000);
        builder.HasIndex(e => e.Status);
        builder.HasIndex(e => e.ScheduledAt);
        builder.Ignore(e => e.CreatedAt);
        builder.Ignore(e => e.UpdatedAt);
    }
}
