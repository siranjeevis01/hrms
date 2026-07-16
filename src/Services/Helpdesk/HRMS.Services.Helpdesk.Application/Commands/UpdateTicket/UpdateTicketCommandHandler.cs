using HRMS.Services.Helpdesk.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Commands.UpdateTicket;

public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand>
{
    private readonly IHelpdeskDbContext _context;

    public UpdateTicketCommandHandler(IHelpdeskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _context.HelpdeskTickets
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (ticket == null)
            throw new InvalidOperationException($"Ticket with ID {request.Id} not found.");

        ticket.Update(request.Subject, request.Description, request.Category, request.Priority, request.DepartmentId);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
