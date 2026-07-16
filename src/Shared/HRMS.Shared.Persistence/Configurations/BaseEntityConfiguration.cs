using HRMS.Shared.Kernel.Common;
using HRMS.Shared.Kernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Shared.Persistence.Configurations;

public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime(6)")
            .IsRequired();

        builder.Property(e => e.UpdatedAt)
            .HasColumnType("datetime(6)");

        builder.Property(e => e.CreatedBy)
            .HasMaxLength(256);

        builder.Property(e => e.UpdatedBy)
            .HasMaxLength(256);

        builder.Property(e => e.IsDeleted)
            .HasColumnType("tinyint(1)")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.TenantId)
            .IsRequired();

        builder.Property(e => e.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.HasIndex(e => e.TenantId);
        builder.HasIndex(e => e.IsDeleted);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
