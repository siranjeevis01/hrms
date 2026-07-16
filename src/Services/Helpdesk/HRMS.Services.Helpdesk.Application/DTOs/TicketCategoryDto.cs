namespace HRMS.Services.Helpdesk.Application.DTOs;

public class TicketCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? DefaultAssigneeId { get; set; }
    public int SLAHours { get; set; }
    public bool IsActive { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
