using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Identity.Application.DTOs;

public class AuditLogDto
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public string? Details { get; set; }
    public DateTime Timestamp { get; set; }
    public bool IsSuccess { get; set; }
    public string? FailureReason { get; set; }
}
