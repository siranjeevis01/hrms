using HRMS.Services.Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Chat.Infrastructure.Persistence.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("Messages");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Content)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(e => e.ConversationId)
            .IsRequired();

        builder.Property(e => e.SenderId)
            .IsRequired();

        builder.HasIndex(e => e.ConversationId);
        builder.HasIndex(e => e.SenderId);
        builder.HasIndex(e => e.CreatedAt);
        builder.HasIndex(e => e.ParentMessageId);
        builder.HasIndex(e => e.TenantId);

        builder.HasMany(e => e.Reactions)
            .WithOne()
            .HasForeignKey(r => r.MessageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Reads)
            .WithOne()
            .HasForeignKey(r => r.MessageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
