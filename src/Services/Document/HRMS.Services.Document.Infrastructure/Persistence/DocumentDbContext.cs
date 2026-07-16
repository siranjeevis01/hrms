using HRMS.Services.Document.Application.Interfaces;
using HRMS.Shared.Kernel.Common;
using Microsoft.EntityFrameworkCore;
using DocEntities = HRMS.Services.Document.Domain.Entities;

namespace HRMS.Services.Document.Infrastructure.Persistence;

public class DocumentDbContext : DbContext, IDocumentDbContext
{
    public DocumentDbContext(DbContextOptions<DocumentDbContext> options) : base(options) { }

    public DbSet<DocEntities.Document> Documents => Set<DocEntities.Document>();
    public DbSet<DocEntities.DocumentFolder> DocumentFolders => Set<DocEntities.DocumentFolder>();
    public DbSet<DocEntities.DocumentVersion> DocumentVersions => Set<DocEntities.DocumentVersion>();
    public DbSet<DocEntities.DocumentShare> DocumentShares => Set<DocEntities.DocumentShare>();
    public DbSet<DocEntities.DocumentAccessLog> DocumentAccessLogs => Set<DocEntities.DocumentAccessLog>();
    public DbSet<DocEntities.DocumentTemplate> DocumentTemplates => Set<DocEntities.DocumentTemplate>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DocumentDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
