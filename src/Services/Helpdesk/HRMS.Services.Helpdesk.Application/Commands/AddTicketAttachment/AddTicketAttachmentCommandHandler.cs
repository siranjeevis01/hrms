using HRMS.Services.Helpdesk.Application.Interfaces;
using HRMS.Services.Helpdesk.Domain.Entities;
using MediatR;

namespace HRMS.Services.Helpdesk.Application.Commands.AddTicketAttachment;

public class AddTicketAttachmentCommandHandler : IRequestHandler<AddTicketAttachmentCommand, Guid>
{
    private readonly IHelpdeskDbContext _context;

    public AddTicketAttachmentCommandHandler(IHelpdeskDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddTicketAttachmentCommand request, CancellationToken cancellationToken)
    {
        var attachment = TicketAttachment.Create(
            request.TicketId,
            request.FileName,
            request.FileUrl,
            request.FileSize,
            request.ContentType,
            request.TenantId);

        _context.TicketAttachments.Add(attachment);
        await _context.SaveChangesAsync(cancellationToken);

        return attachment.Id;
    }
}
