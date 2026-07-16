using HRMS.Services.Expense.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Expense.Infrastructure.Persistence.Configurations;

public class ExpenseCategoryConfiguration : IEntityTypeConfiguration<ExpenseCategoryEntity>
{
    public void Configure(EntityTypeBuilder<ExpenseCategoryEntity> builder)
    {
        builder.ToTable("ExpenseCategories");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Code)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(c => c.Description)
            .HasMaxLength(500);

        builder.Property(c => c.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(c => c.Code);
        builder.HasIndex(c => c.IsActive);
        builder.HasIndex(c => c.TenantId);
    }
}
