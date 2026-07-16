using HRMS.Services.Helpdesk.Application.DTOs;
using MediatR;

namespace HRMS.Services.Helpdesk.Application.Queries.GetTicketAttachments;

public class GetTicketAttachmentsQuery : IRequest<List<TicketAttachmentDto>>
{
    public Guid TicketId { get; set; }
}
