using HRMS.Services.Notification.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Notification.Infrastructure.Persistence.Configurations;

public class SmsQueueConfiguration : IEntityTypeConfiguration<SmsQueue>
{
    public void Configure(EntityTypeBuilder<SmsQueue> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.Property(s => s.Message).IsRequired().HasMaxLength(1600);
        builder.Property(s => s.Provider).IsRequired().HasMaxLength(50);
        builder.Property(s => s.ProviderMessageId).HasMaxLength(200);
        builder.Property(s => s.LastError).HasMaxLength(1000);
        builder.HasIndex(s => s.Status);
        builder.Ignore(s => s.CreatedAt);
        builder.Ignore(s => s.UpdatedAt);
    }
}
