using HRMS.Services.Recruitment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Recruitment.Infrastructure.Persistence.Configurations;

public class OnboardingChecklistConfiguration : IEntityTypeConfiguration<OnboardingChecklist>
{
    public void Configure(EntityTypeBuilder<OnboardingChecklist> builder)
    {
        builder.ToTable("OnboardingChecklists");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Items)
            .IsRequired()
            .HasMaxLength(4000);

        builder.HasIndex(o => o.EmployeeId);
        builder.HasIndex(o => o.CandidateId);
        builder.HasIndex(o => o.TenantId);

        builder.Ignore(o => o.DomainEvents);
    }
}
