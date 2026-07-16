namespace HRMS.Services.Dashboard.Application.DTOs;

public class DashboardShareDto
{
    public Guid Id { get; set; }
    public Guid DashboardId { get; set; }
    public Guid SharedWithUserId { get; set; }
    public string Permission { get; set; } = string.Empty;
    public DateTime SharedAt { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
