using HRMS.Services.Helpdesk.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Commands.CloseTicket;

public class CloseTicketCommandHandler : IRequestHandler<CloseTicketCommand>
{
    private readonly IHelpdeskDbContext _context;

    public CloseTicketCommandHandler(IHelpdeskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CloseTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _context.HelpdeskTickets
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (ticket == null)
            throw new InvalidOperationException($"Ticket with ID {request.Id} not found.");

        ticket.Close();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
