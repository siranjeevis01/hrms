using HRMS.Services.Helpdesk.Domain.Enums;
using MediatR;

namespace HRMS.Services.Helpdesk.Application.Commands.UpdateTicket;

public class UpdateTicketCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Subject { get; set; }
    public string? Description { get; set; }
    public TicketCategoryType? Category { get; set; }
    public TicketPriority? Priority { get; set; }
    public Guid? DepartmentId { get; set; }
}
