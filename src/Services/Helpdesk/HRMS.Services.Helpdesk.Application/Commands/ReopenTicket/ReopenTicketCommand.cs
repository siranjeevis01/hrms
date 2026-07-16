using MediatR;

namespace HRMS.Services.Helpdesk.Application.Commands.ReopenTicket;

public class ReopenTicketCommand : IRequest
{
    public Guid Id { get; set; }
}
