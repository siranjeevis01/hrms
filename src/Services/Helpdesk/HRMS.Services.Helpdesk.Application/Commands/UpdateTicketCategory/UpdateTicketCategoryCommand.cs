using MediatR;

namespace HRMS.Services.Helpdesk.Application.Commands.UpdateTicketCategory;

public class UpdateTicketCategoryCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public Guid? DefaultAssigneeId { get; set; }
    public int? SLAHours { get; set; }
    public bool? IsActive { get; set; }
}
