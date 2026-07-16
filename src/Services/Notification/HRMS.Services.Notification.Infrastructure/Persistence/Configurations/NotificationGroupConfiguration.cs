using HRMS.Services.Notification.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Notification.Infrastructure.Persistence.Configurations;

public class NotificationGroupConfiguration : IEntityTypeConfiguration<NotificationGroup>
{
    public void Configure(EntityTypeBuilder<NotificationGroup> builder)
    {
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Name).IsRequired().HasMaxLength(200);
        builder.Property(g => g.Description).HasMaxLength(1000);
        builder.Property(g => g.Members).HasMaxLength(4000);
        builder.HasIndex(g => g.Name);
        builder.Ignore(g => g.CreatedAt);
        builder.Ignore(g => g.UpdatedAt);
    }
}
