using HRMS.Services.Expense.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Expense.Infrastructure.Persistence.Configurations;

public class ExpensePolicyConfiguration : IEntityTypeConfiguration<ExpensePolicy>
{
    public void Configure(EntityTypeBuilder<ExpensePolicy> builder)
    {
        builder.ToTable("ExpensePolicies");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .HasMaxLength(1000);

        builder.Property(p => p.MaxAmount)
            .HasPrecision(18, 2);

        builder.Property(p => p.Currency)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(p => p.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(p => p.Category);
        builder.HasIndex(p => p.IsActive);
        builder.HasIndex(p => p.TenantId);

        builder.Ignore(p => p.DomainEvents);
    }
}
