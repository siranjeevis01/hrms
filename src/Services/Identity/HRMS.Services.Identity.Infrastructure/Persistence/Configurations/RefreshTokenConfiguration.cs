using HRMS.Services.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Identity.Infrastructure.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Token)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(e => e.CreatedByIp)
            .IsRequired()
            .HasMaxLength(45);

        builder.Property(e => e.RevokedByIp)
            .HasMaxLength(45);

        builder.Property(e => e.ReplacedByToken)
            .HasMaxLength(500);

        builder.HasIndex(e => e.UserId);

        builder.HasIndex(e => e.Expires);

        builder.HasIndex(e => new { e.Token, e.Revoked });
    }
}
