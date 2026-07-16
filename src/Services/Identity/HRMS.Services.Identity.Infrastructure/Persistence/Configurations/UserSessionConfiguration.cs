using HRMS.Services.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Identity.Infrastructure.Persistence.Configurations;

public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
{
    public void Configure(EntityTypeBuilder<UserSession> builder)
    {
        builder.ToTable("UserSessions");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.DeviceInfo)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(e => e.IpAddress)
            .IsRequired()
            .HasMaxLength(45);

        builder.HasIndex(e => e.UserId);

        builder.HasIndex(e => e.IsActive);
    }
}
