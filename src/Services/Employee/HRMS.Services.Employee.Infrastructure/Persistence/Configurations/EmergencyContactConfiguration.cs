using HRMS.Services.Employee.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Employee.Infrastructure.Persistence.Configurations;

public class EmergencyContactConfiguration : IEntityTypeConfiguration<EmergencyContact>
{
    public void Configure(EntityTypeBuilder<EmergencyContact> builder)
    {
        builder.ToTable("EmergencyContacts");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Relationship)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.SecondaryPhone)
            .HasMaxLength(20);

        builder.Property(e => e.Email)
            .HasMaxLength(256);

        builder.Property(e => e.Address)
            .HasMaxLength(500);

        builder.HasIndex(e => e.EmployeeId);
    }
}
