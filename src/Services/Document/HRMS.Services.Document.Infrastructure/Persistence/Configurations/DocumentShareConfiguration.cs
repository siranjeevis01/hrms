using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DocEntities = HRMS.Services.Document.Domain.Entities;

namespace HRMS.Services.Document.Infrastructure.Persistence.Configurations;

public class DocumentShareConfiguration : IEntityTypeConfiguration<DocEntities.DocumentShare>
{
    public void Configure(EntityTypeBuilder<DocEntities.DocumentShare> builder)
    {
        builder.ToTable("DocumentShares");

        builder.HasKey(s => s.Id);

        builder.Ignore(s => s.TenantId);

        builder.HasIndex(s => s.DocumentId);
        builder.HasIndex(s => s.SharedWithUserId);
    }
}
