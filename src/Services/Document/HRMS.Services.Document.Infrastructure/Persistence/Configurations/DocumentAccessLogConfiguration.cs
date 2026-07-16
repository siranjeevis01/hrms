using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DocEntities = HRMS.Services.Document.Domain.Entities;

namespace HRMS.Services.Document.Infrastructure.Persistence.Configurations;

public class DocumentAccessLogConfiguration : IEntityTypeConfiguration<DocEntities.DocumentAccessLog>
{
    public void Configure(EntityTypeBuilder<DocEntities.DocumentAccessLog> builder)
    {
        builder.ToTable("DocumentAccessLogs");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.IpAddress)
            .HasMaxLength(50);

        builder.Ignore(l => l.TenantId);

        builder.HasIndex(l => l.DocumentId);
        builder.HasIndex(l => l.EmployeeId);
    }
}
