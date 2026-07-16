namespace HRMS.Services.Audit.Application.DTOs;

public class LoginHistoryDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime LoginAt { get; set; }
    public DateTime? LogoutAt { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public string? Device { get; set; }
    public string? Browser { get; set; }
    public bool IsSuccessful { get; set; }
    public string? FailureReason { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
