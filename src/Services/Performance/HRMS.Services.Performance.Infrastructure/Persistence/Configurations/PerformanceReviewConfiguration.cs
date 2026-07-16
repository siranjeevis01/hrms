using HRMS.Services.Performance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Performance.Infrastructure.Persistence.Configurations;

public class PerformanceReviewConfiguration : IEntityTypeConfiguration<PerformanceReview>
{
    public void Configure(EntityTypeBuilder<PerformanceReview> builder)
    {
        builder.ToTable("PerformanceReviews");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.ReviewPeriod)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.Strengths)
            .HasMaxLength(2000);

        builder.Property(e => e.AreasForImprovement)
            .HasMaxLength(2000);

        builder.Property(e => e.Comments)
            .HasMaxLength(2000);

        builder.Property(e => e.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.EmployeeId);
        builder.HasIndex(e => e.ReviewerId);
        builder.HasIndex(e => e.ReviewType);
        builder.HasIndex(e => e.Status);
        builder.HasIndex(e => e.ReviewPeriod);
        builder.HasIndex(e => e.TenantId);

        builder.HasMany(e => e.Criteria)
            .WithOne()
            .HasForeignKey(c => c.PerformanceReviewId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(e => e.DomainEvents);
    }
}
