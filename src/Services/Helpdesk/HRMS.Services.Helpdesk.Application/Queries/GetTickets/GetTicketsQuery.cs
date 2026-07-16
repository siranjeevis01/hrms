using HRMS.Services.Helpdesk.Application.DTOs;
using HRMS.Services.Helpdesk.Domain.Enums;
using MediatR;

namespace HRMS.Services.Helpdesk.Application.Queries.GetTickets;

public class GetTicketsQuery : IRequest<PagedResult<HelpdeskTicketDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? EmployeeId { get; set; }
    public TicketStatus? Status { get; set; }
    public TicketPriority? Priority { get; set; }
    public TicketCategoryType? Category { get; set; }
    public Guid? AssignedTo { get; set; }
    public string? SearchTerm { get; set; }
}
