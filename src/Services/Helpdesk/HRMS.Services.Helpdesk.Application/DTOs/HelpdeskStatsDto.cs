using HRMS.Services.Helpdesk.Domain.Enums;

namespace HRMS.Services.Helpdesk.Application.DTOs;

public class HelpdeskStatsDto
{
    public int TotalTickets { get; set; }
    public int OpenTickets { get; set; }
    public int InProgressTickets { get; set; }
    public int ResolvedTickets { get; set; }
    public int ClosedTickets { get; set; }
    public int UrgentTickets { get; set; }
    public int HighPriorityTickets { get; set; }
    public int UnassignedTickets { get; set; }
    public int TotalArticles { get; set; }
    public int PublishedArticles { get; set; }
    public int TotalFaqs { get; set; }
    public Dictionary<TicketCategoryType, int> TicketsByCategory { get; set; } = new();
    public Dictionary<TicketPriority, int> TicketsByPriority { get; set; } = new();
}
