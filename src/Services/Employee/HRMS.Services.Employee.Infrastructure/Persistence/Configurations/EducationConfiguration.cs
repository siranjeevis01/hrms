using HRMS.Services.Employee.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Employee.Infrastructure.Persistence.Configurations;

public class EducationConfiguration : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder.ToTable("Educations");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Institution)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Degree)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.FieldOfStudy)
            .HasMaxLength(200);

        builder.Property(e => e.Grade)
            .HasMaxLength(20);

        builder.Property(e => e.Percentage)
            .HasPrecision(5, 2);

        builder.Property(e => e.Country)
            .HasMaxLength(100);

        builder.Property(e => e.University)
            .HasMaxLength(200);

        builder.HasIndex(e => e.EmployeeId);
    }
}
