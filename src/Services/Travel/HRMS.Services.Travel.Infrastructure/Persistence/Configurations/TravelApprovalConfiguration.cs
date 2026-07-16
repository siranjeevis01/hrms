using HRMS.Services.Travel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Travel.Infrastructure.Persistence.Configurations;

public class TravelApprovalConfiguration : IEntityTypeConfiguration<TravelApproval>
{
    public void Configure(EntityTypeBuilder<TravelApproval> builder)
    {
        builder.ToTable("TravelApprovals");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Comments)
            .HasMaxLength(500);

        builder.Property(a => a.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(a => a.TravelRequestId);
        builder.HasIndex(a => a.ApproverId);
        builder.HasIndex(a => a.Status);
        builder.HasIndex(a => a.TenantId);
    }
}
