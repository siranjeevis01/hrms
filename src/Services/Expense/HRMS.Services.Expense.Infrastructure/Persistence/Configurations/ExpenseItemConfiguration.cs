using HRMS.Services.Expense.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Expense.Infrastructure.Persistence.Configurations;

public class ExpenseItemConfiguration : IEntityTypeConfiguration<ExpenseItem>
{
    public void Configure(EntityTypeBuilder<ExpenseItem> builder)
    {
        builder.ToTable("ExpenseItems");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(i => i.Amount)
            .HasPrecision(18, 2);

        builder.Property(i => i.Currency)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(i => i.ReceiptUrl)
            .HasMaxLength(500);

        builder.Property(i => i.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(i => i.ClaimId);
        builder.HasIndex(i => i.Category);
        builder.HasIndex(i => i.TenantId);
    }
}
