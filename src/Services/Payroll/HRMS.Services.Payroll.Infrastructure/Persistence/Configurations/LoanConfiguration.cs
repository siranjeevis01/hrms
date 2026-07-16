using HRMS.Services.Payroll.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Payroll.Infrastructure.Persistence.Configurations;

public class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.ToTable("Loans");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.EmployeeId).IsRequired();
        builder.Property(l => l.LoanType).IsRequired().HasConversion<int>();
        builder.Property(l => l.Amount).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(l => l.OutstandingAmount).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(l => l.MonthlyDeduction).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(l => l.StartDate).IsRequired();
        builder.Property(l => l.EndDate).IsRequired(false);
        builder.Property(l => l.Status).IsRequired().HasConversion<int>();
        builder.Property(l => l.TenantId).IsRequired();

        builder.HasMany(l => l.Repayments)
            .WithOne(r => r.Loan)
            .HasForeignKey(r => r.LoanId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(l => new { l.EmployeeId, l.Status });
    }
}
