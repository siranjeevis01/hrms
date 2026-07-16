using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HRMS.Services.Organization.Domain.Entities;

namespace HRMS.Services.Organization.Infrastructure.Persistence.Configurations;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branches");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(b => b.Phone)
            .HasMaxLength(20);

        builder.Property(b => b.Email)
            .HasMaxLength(200);

        builder.OwnsOne(b => b.Address, a =>
        {
            a.Property(x => x.Street).HasMaxLength(300);
            a.Property(x => x.City).HasMaxLength(100);
            a.Property(x => x.State).HasMaxLength(100);
            a.Property(x => x.Country).HasMaxLength(100);
            a.Property(x => x.PostalCode).HasMaxLength(20);
            a.Property(x => x.Latitude).HasColumnType("decimal(9,6)");
            a.Property(x => x.Longitude).HasColumnType("decimal(9,6)");
        });

        builder.Property(b => b.CompanyId)
            .IsRequired();

        builder.HasIndex(b => b.CompanyId);
        builder.HasIndex(b => b.Code);
        builder.HasIndex(b => b.TenantId);
    }
}
