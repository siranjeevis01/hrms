using HRMS.Services.Notification.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Notification.Infrastructure.Persistence.Configurations;

public class NotificationTemplateConfiguration : IEntityTypeConfiguration<NotificationTemplate>
{
    public void Configure(EntityTypeBuilder<NotificationTemplate> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
        builder.Property(t => t.Subject).HasMaxLength(200);
        builder.Property(t => t.Body).IsRequired().HasMaxLength(4000);
        builder.Property(t => t.Variables).HasMaxLength(2000);
        builder.Property(t => t.Language).HasMaxLength(10);
        builder.HasIndex(t => t.Name).IsUnique();
        builder.Ignore(t => t.CreatedAt);
        builder.Ignore(t => t.UpdatedAt);
    }
}
