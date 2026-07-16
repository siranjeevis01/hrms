using HRMS.Services.Performance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Performance.Infrastructure.Persistence.Configurations;

public class ReviewCriteriaConfiguration : IEntityTypeConfiguration<ReviewCriteria>
{
    public void Configure(EntityTypeBuilder<ReviewCriteria> builder)
    {
        builder.ToTable("ReviewCriteria");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Category)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.CriteriaName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Comments)
            .HasMaxLength(1000);

        builder.Property(e => e.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.PerformanceReviewId);
    }
}
