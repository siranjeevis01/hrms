using MediatR;

namespace HRMS.Services.Helpdesk.Application.Commands.AssignTicket;

public class AssignTicketCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid AssignedTo { get; set; }
}
