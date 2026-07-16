using MediatR;

namespace HRMS.Services.Helpdesk.Application.Commands.CloseTicket;

public class CloseTicketCommand : IRequest
{
    public Guid Id { get; set; }
}
