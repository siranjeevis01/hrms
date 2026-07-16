using HRMS.Services.Performance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Performance.Infrastructure.Persistence.Configurations;

public class KeyResultConfiguration : IEntityTypeConfiguration<KeyResult>
{
    public void Configure(EntityTypeBuilder<KeyResult> builder)
    {
        builder.ToTable("KeyResults");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .HasMaxLength(1000);

        builder.Property(e => e.Unit)
            .HasMaxLength(50);

        builder.Property(e => e.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.GoalId);
    }
}
