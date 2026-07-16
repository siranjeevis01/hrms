using HRMS.Services.Performance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Performance.Infrastructure.Persistence.Configurations;

public class FeedbackAnswerConfiguration : IEntityTypeConfiguration<FeedbackAnswer>
{
    public void Configure(EntityTypeBuilder<FeedbackAnswer> builder)
    {
        builder.ToTable("FeedbackAnswers");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Question)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(e => e.Comments)
            .HasMaxLength(2000);

        builder.Property(e => e.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.Feedback360Id);
    }
}
