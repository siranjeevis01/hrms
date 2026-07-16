using HRMS.Services.Expense.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Expense.Infrastructure.Persistence.Configurations;

public class ExpenseClaimConfiguration : IEntityTypeConfiguration<ExpenseClaim>
{
    public void Configure(EntityTypeBuilder<ExpenseClaim> builder)
    {
        builder.ToTable("ExpenseClaims");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Description)
            .HasMaxLength(1000);

        builder.Property(c => c.TotalAmount)
            .HasPrecision(18, 2);

        builder.Property(c => c.Currency)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(c => c.RejectionReason)
            .HasMaxLength(500);

        builder.Property(c => c.PolicyViolationNotes)
            .HasMaxLength(500);

        builder.Property(c => c.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(c => c.EmployeeId);
        builder.HasIndex(c => c.Status);
        builder.HasIndex(c => c.TenantId);

        builder.HasMany(c => c.Items)
            .WithOne()
            .HasForeignKey(i => i.ClaimId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Approvals)
            .WithOne()
            .HasForeignKey(a => a.ClaimId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(c => c.DomainEvents);
    }
}
