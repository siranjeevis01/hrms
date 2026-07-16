using HRMS.Services.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Identity.Infrastructure.Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(320);

        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(e => e.ProfilePictureUrl)
            .HasMaxLength(500);

        builder.Property(e => e.FirebaseUid)
            .HasMaxLength(128);

        builder.Property(e => e.LastLoginIp)
            .HasMaxLength(45);

        builder.Property(e => e.MfaSecret)
            .HasMaxLength(128);

        builder.HasIndex(e => e.Email)
            .IsUnique();

        builder.HasIndex(e => e.FirebaseUid)
            .IsUnique();

        builder.HasIndex(e => e.PhoneNumber);

        builder.HasIndex(e => new { e.TenantId, e.Email });
    }
}
