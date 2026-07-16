namespace HRMS.Services.Helpdesk.Application.DTOs;

public class TicketCommentDto
{
    public Guid Id { get; set; }
    public Guid TicketId { get; set; }
    public Guid EmployeeId { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsInternal { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
