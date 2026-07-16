using HRMS.Services.Helpdesk.Application.DTOs;
using MediatR;

namespace HRMS.Services.Helpdesk.Application.Queries.GetTicket;

public class GetTicketQuery : IRequest<HelpdeskTicketDto?>
{
    public Guid Id { get; set; }
}
