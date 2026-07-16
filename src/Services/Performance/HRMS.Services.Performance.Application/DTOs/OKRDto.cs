using HRMS.Services.Performance.Domain.Enums;

namespace HRMS.Services.Performance.Application.DTOs;

public class OKRDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid? ManagerId { get; set; }
    public string Period { get; set; } = string.Empty;
    public OKRStatus Status { get; set; }
    public decimal? OverallScore { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public List<OKRItemDto> Items { get; set; } = new();
}
