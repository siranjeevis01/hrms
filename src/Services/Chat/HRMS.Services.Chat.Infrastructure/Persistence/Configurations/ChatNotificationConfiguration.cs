using HRMS.Services.Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Chat.Infrastructure.Persistence.Configurations;

public class ChatNotificationConfiguration : IEntityTypeConfiguration<ChatNotification>
{
    public void Configure(EntityTypeBuilder<ChatNotification> builder)
    {
        builder.ToTable("ChatNotifications");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.EmployeeId)
            .IsRequired();

        builder.Property(e => e.ConversationId)
            .IsRequired();

        builder.Property(e => e.MessageId)
            .IsRequired();

        builder.HasIndex(e => e.EmployeeId);
        builder.HasIndex(e => e.ConversationId);
        builder.HasIndex(e => e.IsRead);
        builder.HasIndex(e => e.TenantId);
    }
}
