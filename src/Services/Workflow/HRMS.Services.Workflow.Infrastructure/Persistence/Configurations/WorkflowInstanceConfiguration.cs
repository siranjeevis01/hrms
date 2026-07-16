using HRMS.Services.Workflow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Workflow.Infrastructure.Persistence.Configurations;

public class WorkflowInstanceConfiguration : IEntityTypeConfiguration<WorkflowInstance>
{
    public void Configure(EntityTypeBuilder<WorkflowInstance> builder)
    {
        builder.ToTable("WorkflowInstances");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.WorkflowDefinitionId)
            .IsRequired();

        builder.Property(e => e.EntityType)
            .IsRequired();

        builder.Property(e => e.EntityId)
            .IsRequired();

        builder.Property(e => e.RequestedById)
            .IsRequired();

        builder.Property(e => e.CurrentStepNumber)
            .IsRequired();

        builder.Property(e => e.Status)
            .IsRequired();

        builder.Property(e => e.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.WorkflowDefinitionId);
        builder.HasIndex(e => e.EntityType);
        builder.HasIndex(e => e.EntityId);
        builder.HasIndex(e => e.RequestedById);
        builder.HasIndex(e => e.Status);
        builder.HasIndex(e => e.TenantId);

        builder.HasMany(e => e.Actions)
            .WithOne()
            .HasForeignKey(a => a.WorkflowInstanceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(e => e.DomainEvents);
    }
}
