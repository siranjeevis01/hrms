using MediatR;

namespace HRMS.Services.Helpdesk.Application.Commands.AddTicketAttachment;

public class AddTicketAttachmentCommand : IRequest<Guid>
{
    public Guid TicketId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public string TenantId { get; set; } = string.Empty;
}
