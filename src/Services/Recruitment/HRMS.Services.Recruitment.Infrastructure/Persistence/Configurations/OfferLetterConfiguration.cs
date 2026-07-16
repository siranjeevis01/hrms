using HRMS.Services.Recruitment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Recruitment.Infrastructure.Persistence.Configurations;

public class OfferLetterConfiguration : IEntityTypeConfiguration<OfferLetter>
{
    public void Configure(EntityTypeBuilder<OfferLetter> builder)
    {
        builder.ToTable("OfferLetters");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Position)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(o => o.DocumentUrl)
            .HasMaxLength(500);

        builder.Property(o => o.RejectionReason)
            .HasMaxLength(500);

        builder.HasIndex(o => o.Status);
        builder.HasIndex(o => o.CandidateId);
        builder.HasIndex(o => o.TenantId);

        builder.HasOne(o => o.JobApplication)
            .WithMany()
            .HasForeignKey(o => o.JobApplicationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Candidate)
            .WithMany()
            .HasForeignKey(o => o.CandidateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Ignore(o => o.DomainEvents);
    }
}
