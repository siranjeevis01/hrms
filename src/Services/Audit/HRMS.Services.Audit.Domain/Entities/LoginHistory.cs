using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Audit.Domain.Entities;

public class LoginHistory : BaseEntity
{
    public Guid UserId { get; private set; }
    public DateTime LoginAt { get; private set; }
    public DateTime? LogoutAt { get; private set; }
    public string? IpAddress { get; private set; }
    public string? UserAgent { get; private set; }
    public string? Device { get; private set; }
    public string? Browser { get; private set; }
    public bool IsSuccessful { get; private set; }
    public string? FailureReason { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private LoginHistory() { }

    public static LoginHistory CreateLogin(
        Guid userId,
        string? ipAddress,
        string? userAgent,
        string? device,
        string? browser,
        bool isSuccessful,
        string? failureReason,
        string tenantId)
    {
        return new LoginHistory
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            LoginAt = DateTime.UtcNow,
            IpAddress = ipAddress,
            UserAgent = userAgent,
            Device = device,
            Browser = browser,
            IsSuccessful = isSuccessful,
            FailureReason = failureReason,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void RecordLogout()
    {
        LogoutAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
