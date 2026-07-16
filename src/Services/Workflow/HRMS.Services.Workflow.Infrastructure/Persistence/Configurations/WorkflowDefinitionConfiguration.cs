using HRMS.Services.Workflow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Workflow.Infrastructure.Persistence.Configurations;

public class WorkflowDefinitionConfiguration : IEntityTypeConfiguration<WorkflowDefinition>
{
    public void Configure(EntityTypeBuilder<WorkflowDefinition> builder)
    {
        builder.ToTable("WorkflowDefinitions");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .HasMaxLength(1000);

        builder.Property(e => e.EntityType)
            .IsRequired();

        builder.Property(e => e.Steps)
            .HasMaxLength(4000);

        builder.Property(e => e.IsActive)
            .IsRequired();

        builder.Property(e => e.Version)
            .IsRequired();

        builder.Property(e => e.CreatedBy)
            .HasMaxLength(100);

        builder.Property(e => e.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.EntityType);
        builder.HasIndex(e => e.IsActive);
        builder.HasIndex(e => e.TenantId);

        builder.Ignore(e => e.DomainEvents);
    }
}
