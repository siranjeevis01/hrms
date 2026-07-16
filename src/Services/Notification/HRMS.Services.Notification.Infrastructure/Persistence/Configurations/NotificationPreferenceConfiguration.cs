using HRMS.Services.Notification.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Notification.Infrastructure.Persistence.Configurations;

public class NotificationPreferenceConfiguration : IEntityTypeConfiguration<NotificationPreference>
{
    public void Configure(EntityTypeBuilder<NotificationPreference> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => new { p.UserId, p.Category, p.Channel }).IsUnique();
        builder.Ignore(p => p.CreatedAt);
        builder.Ignore(p => p.UpdatedAt);
    }
}
