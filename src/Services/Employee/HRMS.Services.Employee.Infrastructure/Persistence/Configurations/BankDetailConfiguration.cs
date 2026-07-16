using HRMS.Services.Employee.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Employee.Infrastructure.Persistence.Configurations;

public class BankDetailConfiguration : IEntityTypeConfiguration<BankDetail>
{
    public void Configure(EntityTypeBuilder<BankDetail> builder)
    {
        builder.ToTable("BankDetails");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.BankName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.BankCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(b => b.AccountNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(b => b.AccountHolderName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.TaxJurisdiction)
            .HasMaxLength(50);

        builder.Property(b => b.Currency)
            .HasMaxLength(10);

        builder.HasIndex(b => b.EmployeeId);
    }
}
