using HRMS.Services.Employee.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Employee.Infrastructure.Persistence.Configurations;

public class CertificationConfiguration : IEntityTypeConfiguration<Certification>
{
    public void Configure(EntityTypeBuilder<Certification> builder)
    {
        builder.ToTable("Certifications");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.IssuingOrganization)
            .HasMaxLength(200);

        builder.Property(c => c.CredentialId)
            .HasMaxLength(200);

        builder.Property(c => c.CredentialUrl)
            .HasMaxLength(500);

        builder.HasIndex(c => c.EmployeeId);
    }
}
