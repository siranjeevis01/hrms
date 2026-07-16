namespace HRMS.Services.Report.Application.DTOs;

public class ReportAccessDto
{
    public Guid Id { get; set; }
    public Guid TemplateId { get; set; }
    public Guid UserId { get; set; }
    public string Permission { get; set; } = string.Empty;
    public DateTime GrantedAt { get; set; }
    public Guid GrantedBy { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
