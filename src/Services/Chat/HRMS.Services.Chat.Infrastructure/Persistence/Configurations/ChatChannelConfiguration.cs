using HRMS.Services.Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Chat.Infrastructure.Persistence.Configurations;

public class ChatChannelConfiguration : IEntityTypeConfiguration<ChatChannel>
{
    public void Configure(EntityTypeBuilder<ChatChannel> builder)
    {
        builder.ToTable("ChatChannels");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .HasMaxLength(1000);

        builder.Property(e => e.CreatorId)
            .IsRequired();

        builder.HasIndex(e => e.Name);
        builder.HasIndex(e => e.Type);
        builder.HasIndex(e => e.CreatorId);
        builder.HasIndex(e => e.TenantId);

        builder.Ignore(e => e.DomainEvents);
    }
}
