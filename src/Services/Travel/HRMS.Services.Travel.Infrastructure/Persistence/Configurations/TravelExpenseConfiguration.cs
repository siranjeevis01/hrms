using HRMS.Services.Travel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Travel.Infrastructure.Persistence.Configurations;

public class TravelExpenseConfiguration : IEntityTypeConfiguration<TravelExpense>
{
    public void Configure(EntityTypeBuilder<TravelExpense> builder)
    {
        builder.ToTable("TravelExpenses");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Category)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(e => e.Amount)
            .HasPrecision(18, 2);

        builder.Property(e => e.Currency)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(e => e.ReceiptUrl)
            .HasMaxLength(500);

        builder.Property(e => e.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.TravelRequestId);
        builder.HasIndex(e => e.TenantId);
    }
}
