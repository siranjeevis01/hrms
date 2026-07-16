using HRMS.Services.Helpdesk.Domain.Enums;
using MediatR;

namespace HRMS.Services.Helpdesk.Application.Commands.ChangeTicketPriority;

public class ChangeTicketPriorityCommand : IRequest
{
    public Guid Id { get; set; }
    public TicketPriority Priority { get; set; }
}
