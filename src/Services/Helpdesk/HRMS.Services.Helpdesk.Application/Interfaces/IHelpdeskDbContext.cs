using HRMS.Services.Helpdesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Interfaces;

public interface IHelpdeskDbContext
{
    DbSet<HelpdeskTicket> HelpdeskTickets { get; }
    DbSet<TicketComment> TicketComments { get; }
    DbSet<TicketAttachment> TicketAttachments { get; }
    DbSet<TicketCategoryEntity> TicketCategories { get; }
    DbSet<KnowledgeArticle> KnowledgeArticles { get; }
    DbSet<Faq> Faqs { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
