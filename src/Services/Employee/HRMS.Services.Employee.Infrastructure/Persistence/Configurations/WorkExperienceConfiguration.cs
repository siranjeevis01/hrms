using HRMS.Services.Employee.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Employee.Infrastructure.Persistence.Configurations;

public class WorkExperienceConfiguration : IEntityTypeConfiguration<WorkExperience>
{
    public void Configure(EntityTypeBuilder<WorkExperience> builder)
    {
        builder.ToTable("WorkExperiences");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.CompanyName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(w => w.Designation)
            .HasMaxLength(200);

        builder.Property(w => w.Description)
            .HasMaxLength(2000);

        builder.Property(w => w.ReasonForLeaving)
            .HasMaxLength(500);

        builder.Property(w => w.ReferenceName)
            .HasMaxLength(200);

        builder.Property(w => w.ReferencePhone)
            .HasMaxLength(20);

        builder.HasIndex(w => w.EmployeeId);
    }
}
