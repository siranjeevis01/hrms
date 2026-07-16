using HRMS.Services.Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Chat.Infrastructure.Persistence.Configurations;

public class ConversationParticipantConfiguration : IEntityTypeConfiguration<ConversationParticipant>
{
    public void Configure(EntityTypeBuilder<ConversationParticipant> builder)
    {
        builder.ToTable("ConversationParticipants");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.ConversationId)
            .IsRequired();

        builder.Property(e => e.EmployeeId)
            .IsRequired();

        builder.HasIndex(e => e.ConversationId);
        builder.HasIndex(e => e.EmployeeId);
        builder.HasIndex(e => new { e.ConversationId, e.EmployeeId }).IsUnique();
        builder.HasIndex(e => e.TenantId);
    }
}
