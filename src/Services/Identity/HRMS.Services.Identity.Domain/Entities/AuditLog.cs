namespace HRMS.Services.Identity.Domain.Entities;

public class AuditLog
{
    public Guid Id { get; private set; }
    public Guid? UserId { get; private set; }
    public string Action { get; private set; } = string.Empty;
    public string? IpAddress { get; private set; }
    public string? UserAgent { get; private set; }
    public string? Details { get; private set; }
    public DateTime Timestamp { get; private set; }
    public bool IsSuccess { get; private set; }
    public string? FailureReason { get; private set; }

    private AuditLog() { }

    public static AuditLog Create(
        Guid id,
        string action,
        Guid? userId = null,
        string? ipAddress = null,
        string? userAgent = null,
        string? details = null,
        bool isSuccess = true,
        string? failureReason = null)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Audit log ID cannot be empty.", nameof(id));
        if (string.IsNullOrWhiteSpace(action))
            throw new ArgumentException("Action cannot be empty.", nameof(action));

        return new AuditLog
        {
            Id = id,
            UserId = userId,
            Action = action.Trim(),
            IpAddress = ipAddress?.Trim(),
            UserAgent = userAgent?.Trim(),
            Details = details?.Trim(),
            Timestamp = DateTime.UtcNow,
            IsSuccess = isSuccess,
            FailureReason = failureReason?.Trim()
        };
    }
}
