using HRMS.Services.Helpdesk.Application.DTOs;
using HRMS.Services.Helpdesk.Domain.Enums;
using MediatR;

namespace HRMS.Services.Helpdesk.Application.Queries.GetAssignedTickets;

public class GetAssignedTicketsQuery : IRequest<PagedResult<HelpdeskTicketDto>>
{
    public Guid AssignedTo { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public TicketStatus? Status { get; set; }
}
