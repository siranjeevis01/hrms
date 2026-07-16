using HRMS.Services.Helpdesk.Domain.Enums;
using MediatR;

namespace HRMS.Services.Helpdesk.Application.Commands.CreateTicket;

public class CreateTicketCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TicketCategoryType Category { get; set; }
    public TicketPriority Priority { get; set; }
    public Guid? DepartmentId { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
