using HRMS.Services.Notification.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Notification.Infrastructure.Persistence.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<NotificationEntity>
{
    public void Configure(EntityTypeBuilder<NotificationEntity> builder)
    {
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Title).IsRequired().HasMaxLength(200);
        builder.Property(n => n.Message).IsRequired().HasMaxLength(2000);
        builder.Property(n => n.FailureReason).HasMaxLength(500);
        builder.Property(n => n.ActionUrl).HasMaxLength(500);
        builder.Property(n => n.Data).HasMaxLength(4000);
        builder.HasIndex(n => n.UserId);
        builder.HasIndex(n => n.Status);
        builder.HasIndex(n => n.Category);
        builder.HasIndex(n => n.CreatedAt);
        builder.Ignore(n => n.CreatedAt);
        builder.Ignore(n => n.UpdatedAt);
    }
}
