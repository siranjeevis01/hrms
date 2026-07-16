using HRMS.Services.Helpdesk.Application.Interfaces;
using HRMS.Services.Helpdesk.Domain.Entities;
using MediatR;

namespace HRMS.Services.Helpdesk.Application.Commands.CreateTicket;

public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Guid>
{
    private readonly IHelpdeskDbContext _context;

    public CreateTicketCommandHandler(IHelpdeskDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = HelpdeskTicket.Create(
            request.EmployeeId,
            request.Subject,
            request.Description,
            request.Category,
            request.Priority,
            request.DepartmentId,
            request.TenantId);

        _context.HelpdeskTickets.Add(ticket);
        await _context.SaveChangesAsync(cancellationToken);

        return ticket.Id;
    }
}
