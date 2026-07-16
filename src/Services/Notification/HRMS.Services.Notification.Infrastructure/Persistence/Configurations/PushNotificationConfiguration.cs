using HRMS.Services.Notification.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Notification.Infrastructure.Persistence.Configurations;

public class PushNotificationConfiguration : IEntityTypeConfiguration<PushNotification>
{
    public void Configure(EntityTypeBuilder<PushNotification> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Body).IsRequired().HasMaxLength(2000);
        builder.Property(p => p.DeviceTokens).HasMaxLength(4000);
        builder.Property(p => p.Response).HasMaxLength(2000);
        builder.HasIndex(p => p.UserId);
        builder.HasIndex(p => p.Status);
        builder.Ignore(p => p.CreatedAt);
        builder.Ignore(p => p.UpdatedAt);
    }
}
