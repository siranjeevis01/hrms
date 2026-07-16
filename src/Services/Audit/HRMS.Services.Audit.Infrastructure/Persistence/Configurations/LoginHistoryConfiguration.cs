using HRMS.Services.Audit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Audit.Infrastructure.Persistence.Configurations;

public class LoginHistoryConfiguration : IEntityTypeConfiguration<LoginHistory>
{
    public void Configure(EntityTypeBuilder<LoginHistory> builder)
    {
        builder.ToTable("LoginHistories");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.UserId)
            .IsRequired();

        builder.Property(l => l.IpAddress)
            .HasMaxLength(45);

        builder.Property(l => l.UserAgent)
            .HasMaxLength(500);

        builder.Property(l => l.Device)
            .HasMaxLength(100);

        builder.Property(l => l.Browser)
            .HasMaxLength(100);

        builder.Property(l => l.FailureReason)
            .HasMaxLength(500);

        builder.Property(l => l.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(l => l.UserId);
        builder.HasIndex(l => l.LoginAt);
        builder.HasIndex(l => l.TenantId);
    }
}
