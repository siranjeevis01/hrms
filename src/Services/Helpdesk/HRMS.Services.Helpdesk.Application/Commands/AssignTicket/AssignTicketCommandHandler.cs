using HRMS.Services.Helpdesk.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Commands.AssignTicket;

public class AssignTicketCommandHandler : IRequestHandler<AssignTicketCommand>
{
    private readonly IHelpdeskDbContext _context;

    public AssignTicketCommandHandler(IHelpdeskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AssignTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _context.HelpdeskTickets
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (ticket == null)
            throw new InvalidOperationException($"Ticket with ID {request.Id} not found.");

        ticket.Assign(request.AssignedTo);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
