using HRMS.Services.Employee.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Employee.Infrastructure.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Domain.Entities.Employee>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Employee> builder)
    {
        builder.ToTable("Employees");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.EmployeeCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.MiddleName)
            .HasMaxLength(100);

        builder.Property(e => e.PreferredName)
            .HasMaxLength(100);

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(e => e.PersonalEmail)
            .HasMaxLength(256);

        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(e => e.Nationality)
            .HasMaxLength(50);

        builder.Property(e => e.BloodGroup)
            .HasMaxLength(10);

        builder.Property(e => e.ProfilePictureUrl)
            .HasMaxLength(500);

        builder.Property(e => e.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.EmployeeCode).IsUnique();
        builder.HasIndex(e => e.UserId);
        builder.HasIndex(e => e.CompanyId);
        builder.HasIndex(e => e.DepartmentId);
        builder.HasIndex(e => e.DesignationId);
        builder.HasIndex(e => e.EmploymentStatus);
        builder.HasIndex(e => e.TenantId);

        builder.HasMany(e => e.EmergencyContacts)
            .WithOne()
            .HasForeignKey(c => c.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Documents)
            .WithOne()
            .HasForeignKey(d => d.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.BankDetails)
            .WithOne()
            .HasForeignKey(b => b.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Educations)
            .WithOne()
            .HasForeignKey(ed => ed.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.WorkExperiences)
            .WithOne()
            .HasForeignKey(w => w.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Certifications)
            .WithOne()
            .HasForeignKey(c => c.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Skills)
            .WithOne()
            .HasForeignKey(s => s.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.SalaryStructures)
            .WithOne()
            .HasForeignKey(s => s.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Histories)
            .WithOne()
            .HasForeignKey(h => h.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Dependents)
            .WithOne()
            .HasForeignKey(d => d.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(e => e.DomainEvents);
    }
}
