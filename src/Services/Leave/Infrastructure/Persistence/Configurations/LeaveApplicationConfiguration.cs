using HRMS.Services.Leave.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Leave.Infrastructure.Persistence.Configurations;

public class LeaveApplicationConfiguration : IEntityTypeConfiguration<LeaveApplication>
{
    public void Configure(EntityTypeBuilder<LeaveApplication> builder)
    {
        builder.ToTable("LeaveApplications");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.EmployeeId)
            .IsRequired();

        builder.Property(e => e.LeaveTypeId)
            .IsRequired();

        builder.Property(e => e.StartDate)
            .IsRequired();

        builder.Property(e => e.EndDate)
            .IsRequired();

        builder.Property(e => e.TotalDays)
            .HasPrecision(5, 2);

        builder.Property(e => e.Reason)
            .HasMaxLength(2000);

        builder.Property(e => e.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(e => e.HalfDayType)
            .HasConversion<int?>();

        builder.Property(e => e.RejectionReason)
            .HasMaxLength(1000);

        builder.Property(e => e.TenantId)
            .IsRequired();

        builder.HasMany(e => e.Comments)
            .WithOne()
            .HasForeignKey(c => c.LeaveApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => e.EmployeeId);
        builder.HasIndex(e => e.Status);
        builder.HasIndex(e => e.TenantId);
        builder.HasIndex(e => new { e.StartDate, e.EndDate });
    }
}
