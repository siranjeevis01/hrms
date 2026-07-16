using HRMS.Services.Attendance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Attendance.Infrastructure.Persistence.Configurations;

public class WorkFromHomeConfiguration : IEntityTypeConfiguration<WorkFromHome>
{
    public void Configure(EntityTypeBuilder<WorkFromHome> builder)
    {
        builder.ToTable("WorkFromHomes");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.EmployeeId)
            .IsRequired();

        builder.Property(w => w.StartDate)
            .IsRequired();

        builder.Property(w => w.EndDate)
            .IsRequired();

        builder.Property(w => w.Reason)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(w => w.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(w => w.DayWiseStatus)
            .HasMaxLength(2000);

        builder.Property(w => w.TenantId)
            .IsRequired();

        builder.HasIndex(w => w.EmployeeId);
        builder.HasIndex(w => w.Status);
        builder.HasIndex(w => w.TenantId);
    }
}
