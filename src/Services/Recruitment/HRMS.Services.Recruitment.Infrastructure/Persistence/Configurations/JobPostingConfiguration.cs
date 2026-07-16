using HRMS.Services.Recruitment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Recruitment.Infrastructure.Persistence.Configurations;

public class JobPostingConfiguration : IEntityTypeConfiguration<JobPosting>
{
    public void Configure(EntityTypeBuilder<JobPosting> builder)
    {
        builder.ToTable("JobPostings");

        builder.HasKey(j => j.Id);

        builder.Property(j => j.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(j => j.Description)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(j => j.Currency)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(j => j.Skills)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(j => j.Requirements)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(j => j.Responsibilities)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(j => j.Benefits)
            .IsRequired()
            .HasMaxLength(4000);

        builder.HasIndex(j => j.Status);
        builder.HasIndex(j => j.DepartmentId);
        builder.HasIndex(j => j.DesignationId);
        builder.HasIndex(j => j.HiringManagerId);
        builder.HasIndex(j => j.RecruiterId);
        builder.HasIndex(j => j.TenantId);

        builder.Ignore(j => j.DomainEvents);
    }
}
