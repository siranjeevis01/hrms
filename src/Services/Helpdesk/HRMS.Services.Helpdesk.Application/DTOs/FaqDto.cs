namespace HRMS.Services.Helpdesk.Application.DTOs;

public class FaqDto
{
    public Guid Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public Guid? CategoryId { get; set; }
    public int Order { get; set; }
    public bool IsActive { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
