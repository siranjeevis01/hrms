using MediatR;

namespace HRMS.Services.Helpdesk.Application.Commands.ResolveTicket;

public class ResolveTicketCommand : IRequest
{
    public Guid Id { get; set; }
    public string? ResolutionNotes { get; set; }
}
