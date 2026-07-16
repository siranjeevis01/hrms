using HRMS.Services.Helpdesk.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Commands.ChangeTicketPriority;

public class ChangeTicketPriorityCommandHandler : IRequestHandler<ChangeTicketPriorityCommand>
{
    private readonly IHelpdeskDbContext _context;

    public ChangeTicketPriorityCommandHandler(IHelpdeskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ChangeTicketPriorityCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _context.HelpdeskTickets
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (ticket == null)
            throw new InvalidOperationException($"Ticket with ID {request.Id} not found.");

        ticket.ChangePriority(request.Priority);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
