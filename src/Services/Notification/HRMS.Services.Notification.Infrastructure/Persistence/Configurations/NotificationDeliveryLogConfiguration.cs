using HRMS.Services.Notification.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Notification.Infrastructure.Persistence.Configurations;

public class NotificationDeliveryLogConfiguration : IEntityTypeConfiguration<NotificationDeliveryLog>
{
    public void Configure(EntityTypeBuilder<NotificationDeliveryLog> builder)
    {
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Provider).IsRequired().HasMaxLength(50);
        builder.Property(l => l.ProviderMessageId).HasMaxLength(200);
        builder.Property(l => l.Response).HasMaxLength(2000);
        builder.HasIndex(l => l.NotificationId);
        builder.Ignore(l => l.CreatedAt);
        builder.Ignore(l => l.UpdatedAt);
    }
}
