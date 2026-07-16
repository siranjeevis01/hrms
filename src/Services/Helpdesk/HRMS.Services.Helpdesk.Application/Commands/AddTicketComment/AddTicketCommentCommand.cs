using MediatR;

namespace HRMS.Services.Helpdesk.Application.Commands.AddTicketComment;

public class AddTicketCommentCommand : IRequest<Guid>
{
    public Guid TicketId { get; set; }
    public Guid EmployeeId { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsInternal { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
