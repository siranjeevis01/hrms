using HRMS.Services.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Identity.Infrastructure.Persistence.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolePermissions");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Permission)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Module)
            .HasMaxLength(100);

        builder.Property(e => e.Description)
            .HasMaxLength(500);

        builder.HasIndex(e => new { e.RoleId, e.Permission })
            .IsUnique();
    }
}
