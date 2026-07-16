using HRMS.Services.Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Chat.Infrastructure.Persistence.Configurations;

public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
{
    public void Configure(EntityTypeBuilder<Conversation> builder)
    {
        builder.ToTable("Conversations");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .HasMaxLength(1000);

        builder.Property(e => e.CreatorId)
            .IsRequired();

        builder.HasIndex(e => e.Type);
        builder.HasIndex(e => e.CreatorId);
        builder.HasIndex(e => e.TenantId);
        builder.HasIndex(e => e.LastMessageAt);

        builder.Ignore(e => e.DomainEvents);
    }
}
