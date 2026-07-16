using HRMS.Services.Helpdesk.Application.DTOs;
using MediatR;

namespace HRMS.Services.Helpdesk.Application.Queries.GetTicketComments;

public class GetTicketCommentsQuery : IRequest<List<TicketCommentDto>>
{
    public Guid TicketId { get; set; }
}
