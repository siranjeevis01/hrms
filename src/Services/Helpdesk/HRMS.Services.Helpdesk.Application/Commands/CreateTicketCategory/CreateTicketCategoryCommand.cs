using MediatR;

namespace HRMS.Services.Helpdesk.Application.Commands.CreateTicketCategory;

public class CreateTicketCategoryCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? DefaultAssigneeId { get; set; }
    public int SLAHours { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
