using HRMS.Services.Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Chat.Infrastructure.Persistence.Configurations;

public class MessageReactionConfiguration : IEntityTypeConfiguration<MessageReaction>
{
    public void Configure(EntityTypeBuilder<MessageReaction> builder)
    {
        builder.ToTable("MessageReactions");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.MessageId)
            .IsRequired();

        builder.Property(e => e.EmployeeId)
            .IsRequired();

        builder.Property(e => e.Emoji)
            .IsRequired()
            .HasMaxLength(10);

        builder.HasIndex(e => e.MessageId);
        builder.HasIndex(e => e.EmployeeId);
        builder.HasIndex(e => new { e.MessageId, e.EmployeeId, e.Emoji }).IsUnique();
        builder.HasIndex(e => e.TenantId);
    }
}
