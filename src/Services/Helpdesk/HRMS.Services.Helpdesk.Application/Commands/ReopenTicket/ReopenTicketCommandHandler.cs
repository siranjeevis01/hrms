using HRMS.Services.Helpdesk.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Commands.ReopenTicket;

public class ReopenTicketCommandHandler : IRequestHandler<ReopenTicketCommand>
{
    private readonly IHelpdeskDbContext _context;

    public ReopenTicketCommandHandler(IHelpdeskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ReopenTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _context.HelpdeskTickets
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (ticket == null)
            throw new InvalidOperationException($"Ticket with ID {request.Id} not found.");

        ticket.Reopen();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
