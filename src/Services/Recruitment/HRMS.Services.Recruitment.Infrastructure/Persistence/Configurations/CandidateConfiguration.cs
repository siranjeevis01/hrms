using HRMS.Services.Recruitment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Recruitment.Infrastructure.Persistence.Configurations;

public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.ToTable("Candidates");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(c => c.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(c => c.CurrentCompany)
            .HasMaxLength(200);

        builder.Property(c => c.CurrentDesignation)
            .HasMaxLength(200);

        builder.Property(c => c.Currency)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(c => c.ResumeUrl)
            .HasMaxLength(500);

        builder.Property(c => c.Skills)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(c => c.Education)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(c => c.Notes)
            .HasMaxLength(2000);

        builder.Property(c => c.RejectionReason)
            .HasMaxLength(500);

        builder.HasIndex(c => c.Email);
        builder.HasIndex(c => c.Status);
        builder.HasIndex(c => c.ReferralEmployeeId);
        builder.HasIndex(c => c.TenantId);

        builder.Ignore(c => c.DomainEvents);
    }
}
