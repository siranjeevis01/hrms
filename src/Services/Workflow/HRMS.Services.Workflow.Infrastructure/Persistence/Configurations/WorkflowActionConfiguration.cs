using HRMS.Services.Workflow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Workflow.Infrastructure.Persistence.Configurations;

public class WorkflowActionConfiguration : IEntityTypeConfiguration<WorkflowAction>
{
    public void Configure(EntityTypeBuilder<WorkflowAction> builder)
    {
        builder.ToTable("WorkflowActions");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.WorkflowInstanceId)
            .IsRequired();

        builder.Property(e => e.StepId)
            .IsRequired();

        builder.Property(e => e.ApproverId)
            .IsRequired();

        builder.Property(e => e.Action)
            .IsRequired();

        builder.Property(e => e.Comments)
            .HasMaxLength(2000);

        builder.Property(e => e.ActionedAt)
            .IsRequired();

        builder.Property(e => e.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.WorkflowInstanceId);
        builder.HasIndex(e => e.StepId);
        builder.HasIndex(e => e.ApproverId);
        builder.HasIndex(e => e.TenantId);

        builder.Ignore(e => e.DomainEvents);
    }
}
