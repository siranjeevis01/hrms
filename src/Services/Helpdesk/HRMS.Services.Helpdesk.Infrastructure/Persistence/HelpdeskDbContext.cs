using HRMS.Services.Helpdesk.Application.Interfaces;
using HRMS.Services.Helpdesk.Domain.Entities;
using HRMS.Shared.Kernel.Common;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Infrastructure.Persistence;

public class HelpdeskDbContext : DbContext, IHelpdeskDbContext
{
    public HelpdeskDbContext(DbContextOptions<HelpdeskDbContext> options) : base(options) { }

    public DbSet<HelpdeskTicket> HelpdeskTickets => Set<HelpdeskTicket>();
    public DbSet<TicketComment> TicketComments => Set<TicketComment>();
    public DbSet<TicketAttachment> TicketAttachments => Set<TicketAttachment>();
    public DbSet<TicketCategoryEntity> TicketCategories => Set<TicketCategoryEntity>();
    public DbSet<KnowledgeArticle> KnowledgeArticles => Set<KnowledgeArticle>();
    public DbSet<Faq> Faqs => Set<Faq>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HelpdeskDbContext).Assembly);
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
