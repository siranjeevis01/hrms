using HRMS.Services.Helpdesk.Application.Interfaces;
using HRMS.Services.Helpdesk.Domain.Entities;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;

namespace HRMS.Services.Helpdesk.Application.Commands.CreateTicket;

public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Guid>
{
    private readonly IHelpdeskDbContext _context;
    private readonly INotificationService _notificationService;

    public CreateTicketCommandHandler(IHelpdeskDbContext context, INotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
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

        var subject = $"New Helpdesk Ticket: {request.Subject}";
        var body = $"""
            <div style="font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;">
                <h2 style="color: #2563eb;">Helpdesk Ticket Created</h2>
                <p>A new helpdesk ticket has been created.</p>
                <table style="width: 100%; border-collapse: collapse; margin: 20px 0;">
                    <tr><td style="padding: 8px; font-weight: bold; border-bottom: 1px solid #eee;">Subject</td><td style="padding: 8px; border-bottom: 1px solid #eee;">{request.Subject}</td></tr>
                    <tr><td style="padding: 8px; font-weight: bold; border-bottom: 1px solid #eee;">Category</td><td style="padding: 8px; border-bottom: 1px solid #eee;">{request.Category}</td></tr>
                    <tr><td style="padding: 8px; font-weight: bold; border-bottom: 1px solid #eee;">Priority</td><td style="padding: 8px; border-bottom: 1px solid #eee;">{request.Priority}</td></tr>
                    <tr><td style="padding: 8px; font-weight: bold; border-bottom: 1px solid #eee;">Description</td><td style="padding: 8px; border-bottom: 1px solid #eee;">{request.Description}</td></tr>
                </table>
                <p style="color: #666; font-size: 14px;">Ticket ID: {ticket.Id}</p>
                <hr style="border: none; border-top: 1px solid #eee; margin: 20px 0;" />
                <p style="color: #999; font-size: 12px;">This email was sent from HRMS Pro.</p>
            </div>
            """;

        await _notificationService.SendEmailAsync(
            $"employee-{request.EmployeeId}@hrms.local",
            subject,
            body,
            true,
            cancellationToken);

        return ticket.Id;
    }
}
