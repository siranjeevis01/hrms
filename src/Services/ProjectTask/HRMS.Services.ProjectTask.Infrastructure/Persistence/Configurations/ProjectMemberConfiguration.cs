using HRMS.Services.ProjectTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.ProjectTask.Infrastructure.Persistence.Configurations;

public class ProjectMemberConfiguration : IEntityTypeConfiguration<ProjectMember>
{
    public void Configure(EntityTypeBuilder<ProjectMember> builder)
    {
        builder.ToTable("ProjectMembers");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.AllocationPercentage)
            .HasColumnType("decimal(5,2)");

        builder.HasIndex(e => e.ProjectId);
        builder.HasIndex(e => e.EmployeeId);
    }
}
