using HRMS.Services.Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Chat.Infrastructure.Persistence.Configurations;

public class UserPresenceConfiguration : IEntityTypeConfiguration<UserPresence>
{
    public void Configure(EntityTypeBuilder<UserPresence> builder)
    {
        builder.ToTable("UserPresences");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.EmployeeId)
            .IsRequired();

        builder.HasIndex(e => e.EmployeeId).IsUnique();
        builder.HasIndex(e => e.PresenceStatus);
        builder.HasIndex(e => e.TenantId);
    }
}
