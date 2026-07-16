using HRMS.Services.Payroll.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Payroll.Infrastructure.Persistence.Configurations;

public class LoanRepaymentConfiguration : IEntityTypeConfiguration<LoanRepayment>
{
    public void Configure(EntityTypeBuilder<LoanRepayment> builder)
    {
        builder.ToTable("LoanRepayments");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.LoanId).IsRequired();
        builder.Property(r => r.EmployeePayrollId).IsRequired();
        builder.Property(r => r.Amount).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(r => r.PaidDate).IsRequired();
        builder.Property(r => r.RemainingBalance).HasColumnType("decimal(18,2)").HasDefaultValue(0);
    }
}
