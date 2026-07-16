using HRMS.Services.Performance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Performance.Infrastructure.Persistence.Configurations;

public class OKRItemConfiguration : IEntityTypeConfiguration<OKRItem>
{
    public void Configure(EntityTypeBuilder<OKRItem> builder)
    {
        builder.ToTable("OKRItems");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.ObjectiveTitle)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.ObjectiveDescription)
            .HasMaxLength(1000);

        builder.Property(e => e.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.OKRId);
    }
}
