using HRMS.Services.Helpdesk.Domain.Enums;

namespace HRMS.Services.Helpdesk.Application.DTOs;

public class HelpdeskTicketDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TicketCategoryType Category { get; set; }
    public TicketPriority Priority { get; set; }
    public TicketStatus Status { get; set; }
    public Guid? AssignedTo { get; set; }
    public Guid? DepartmentId { get; set; }
    public string? ResolutionNotes { get; set; }
    public DateTime? ResolvedAt { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public List<TicketCommentDto> Comments { get; set; } = new();
    public List<TicketAttachmentDto> Attachments { get; set; } = new();
}
