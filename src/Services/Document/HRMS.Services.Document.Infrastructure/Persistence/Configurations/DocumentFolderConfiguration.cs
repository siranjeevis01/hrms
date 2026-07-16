using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DocEntities = HRMS.Services.Document.Domain.Entities;

namespace HRMS.Services.Document.Infrastructure.Persistence.Configurations;

public class DocumentFolderConfiguration : IEntityTypeConfiguration<DocEntities.DocumentFolder>
{
    public void Configure(EntityTypeBuilder<DocEntities.DocumentFolder> builder)
    {
        builder.ToTable("DocumentFolders");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(f => f.Path)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(f => f.Description)
            .HasMaxLength(1000);

        builder.Ignore(f => f.TenantId);

        builder.HasIndex(f => f.ParentFolderId);

        builder.Ignore(f => f.DomainEvents);
    }
}
