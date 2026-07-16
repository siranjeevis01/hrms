using HRMS.Services.Expense.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Expense.Infrastructure.Persistence.Configurations;

public class ExpenseApprovalConfiguration : IEntityTypeConfiguration<ExpenseApproval>
{
    public void Configure(EntityTypeBuilder<ExpenseApproval> builder)
    {
        builder.ToTable("ExpenseApprovals");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Comments)
            .HasMaxLength(500);

        builder.Property(a => a.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(a => a.ClaimId);
        builder.HasIndex(a => a.ApproverId);
        builder.HasIndex(a => a.Status);
        builder.HasIndex(a => a.TenantId);
    }
}
