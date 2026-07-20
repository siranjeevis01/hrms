using HRMS.Services.Helpdesk.Application.Interfaces;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Commands.ResolveTicket;

public class ResolveTicketCommandHandler : IRequestHandler<ResolveTicketCommand>
{
    private readonly IHelpdeskDbContext _context;
    private readonly INotificationService _notificationService;

    public ResolveTicketCommandHandler(IHelpdeskDbContext context, INotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }

    public async Task Handle(ResolveTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _context.HelpdeskTickets
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (ticket == null)
            throw new InvalidOperationException($"Ticket with ID {request.Id} not found.");

        ticket.Resolve(request.ResolutionNotes);
        await _context.SaveChangesAsync(cancellationToken);

        var subject = $"Ticket Resolved: {ticket.Subject}";
        var body = $"""
            <div style="font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;">
                <h2 style="color: #16a34a;">Ticket Resolved</h2>
                <p>Your helpdesk ticket has been resolved.</p>
                <table style="width: 100%; border-collapse: collapse; margin: 20px 0;">
                    <tr><td style="padding: 8px; font-weight: bold; border-bottom: 1px solid #eee;">Subject</td><td style="padding: 8px; border-bottom: 1px solid #eee;">{ticket.Subject}</td></tr>
                    <tr><td style="padding: 8px; font-weight: bold; border-bottom: 1px solid #eee;">Resolution</td><td style="padding: 8px; border-bottom: 1px solid #eee;">{ticket.ResolutionNotes ?? "No notes provided"}</td></tr>
                </table>
                <p style="color: #666; font-size: 14px;">If your issue is not resolved, please reopen the ticket.</p>
                <hr style="border: none; border-top: 1px solid #eee; margin: 20px 0;" />
                <p style="color: #999; font-size: 12px;">This email was sent from HRMS Pro.</p>
            </div>
            """;

        await _notificationService.SendEmailAsync(
            $"employee-{ticket.EmployeeId}@hrms.local",
            subject,
            body,
            true,
            cancellationToken);
    }
}
