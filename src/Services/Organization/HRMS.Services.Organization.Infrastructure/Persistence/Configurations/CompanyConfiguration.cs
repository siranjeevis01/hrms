using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HRMS.Services.Organization.Domain.Entities;

namespace HRMS.Services.Organization.Infrastructure.Persistence.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.LegalName)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(c => c.RegistrationNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.TaxId)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.LogoUrl)
            .HasMaxLength(500);

        builder.Property(c => c.Website)
            .HasMaxLength(200);

        builder.Property(c => c.Email)
            .HasMaxLength(200);

        builder.Property(c => c.Phone)
            .HasMaxLength(20);

        builder.Property(c => c.Industry)
            .HasMaxLength(100);

        builder.Property(c => c.EmployeeCountRange)
            .HasMaxLength(50);

        builder.OwnsOne(c => c.Address, a =>
        {
            a.Property(x => x.Street).HasMaxLength(300);
            a.Property(x => x.City).HasMaxLength(100);
            a.Property(x => x.State).HasMaxLength(100);
            a.Property(x => x.Country).HasMaxLength(100);
            a.Property(x => x.PostalCode).HasMaxLength(20);
            a.Property(x => x.Latitude).HasColumnType("decimal(9,6)");
            a.Property(x => x.Longitude).HasColumnType("decimal(9,6)");
        });

        builder.HasIndex(c => c.TenantId);
        builder.HasIndex(c => c.Name);
        builder.HasIndex(c => c.RegistrationNumber).IsUnique();
    }
}
