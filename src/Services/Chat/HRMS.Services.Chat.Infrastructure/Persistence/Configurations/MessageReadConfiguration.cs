using HRMS.Services.Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Chat.Infrastructure.Persistence.Configurations;

public class MessageReadConfiguration : IEntityTypeConfiguration<MessageRead>
{
    public void Configure(EntityTypeBuilder<MessageRead> builder)
    {
        builder.ToTable("MessageReads");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.MessageId)
            .IsRequired();

        builder.Property(e => e.EmployeeId)
            .IsRequired();

        builder.HasIndex(e => e.MessageId);
        builder.HasIndex(e => e.EmployeeId);
        builder.HasIndex(e => new { e.MessageId, e.EmployeeId }).IsUnique();
        builder.HasIndex(e => e.TenantId);
    }
}
