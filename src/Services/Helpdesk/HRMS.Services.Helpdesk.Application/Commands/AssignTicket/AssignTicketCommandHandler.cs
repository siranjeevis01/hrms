using HRMS.Services.Helpdesk.Application.Interfaces;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Commands.AssignTicket;

public class AssignTicketCommandHandler : IRequestHandler<AssignTicketCommand>
{
    private readonly IHelpdeskDbContext _context;
    private readonly INotificationService _notificationService;

    public AssignTicketCommandHandler(IHelpdeskDbContext context, INotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }

    public async Task Handle(AssignTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _context.HelpdeskTickets
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (ticket == null)
            throw new InvalidOperationException($"Ticket with ID {request.Id} not found.");

        ticket.Assign(request.AssignedTo);
        await _context.SaveChangesAsync(cancellationToken);

        var subject = $"Ticket Assigned: {ticket.Subject}";
        var body = $"""
            <div style="font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;">
                <h2 style="color: #f59e0b;">Ticket Assigned to You</h2>
                <p>A helpdesk ticket has been assigned to you.</p>
                <table style="width: 100%; border-collapse: collapse; margin: 20px 0;">
                    <tr><td style="padding: 8px; font-weight: bold; border-bottom: 1px solid #eee;">Subject</td><td style="padding: 8px; border-bottom: 1px solid #eee;">{ticket.Subject}</td></tr>
                    <tr><td style="padding: 8px; font-weight: bold; border-bottom: 1px solid #eee;">Category</td><td style="padding: 8px; border-bottom: 1px solid #eee;">{ticket.Category}</td></tr>
                    <tr><td style="padding: 8px; font-weight: bold; border-bottom: 1px solid #eee;">Priority</td><td style="padding: 8px; border-bottom: 1px solid #eee;">{ticket.Priority}</td></tr>
                    <tr><td style="padding: 8px; font-weight: bold; border-bottom: 1px solid #eee;">Status</td><td style="padding: 8px; border-bottom: 1px solid #eee;">{ticket.Status}</td></tr>
                </table>
                <p style="color: #666; font-size: 14px;">Ticket ID: {ticket.Id}</p>
                <hr style="border: none; border-top: 1px solid #eee; margin: 20px 0;" />
                <p style="color: #999; font-size: 12px;">This email was sent from HRMS Pro.</p>
            </div>
            """;

        await _notificationService.SendEmailAsync(
            $"employee-{request.AssignedTo}@hrms.local",
            subject,
            body,
            true,
            cancellationToken);
    }
}
