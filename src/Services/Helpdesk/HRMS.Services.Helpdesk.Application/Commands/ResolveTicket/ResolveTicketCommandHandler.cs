using HRMS.Services.Helpdesk.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Commands.ResolveTicket;

public class ResolveTicketCommandHandler : IRequestHandler<ResolveTicketCommand>
{
    private readonly IHelpdeskDbContext _context;

    public ResolveTicketCommandHandler(IHelpdeskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ResolveTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _context.HelpdeskTickets
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (ticket == null)
            throw new InvalidOperationException($"Ticket with ID {request.Id} not found.");

        ticket.Resolve(request.ResolutionNotes);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
