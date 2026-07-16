namespace HRMS.Services.Identity.Domain.Entities;

public class UserSession
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string DeviceInfo { get; private set; } = string.Empty;
    public string IpAddress { get; private set; } = string.Empty;
    public DateTime LastActiveAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public bool IsActive { get; private set; }
    public Guid? RefreshTokenId { get; private set; }

    private UserSession() { }

    public static UserSession Create(
        Guid id,
        Guid userId,
        string deviceInfo,
        string ipAddress,
        DateTime expiresAt,
        Guid? refreshTokenId = null)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Session ID cannot be empty.", nameof(id));
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));
        if (string.IsNullOrWhiteSpace(deviceInfo))
            throw new ArgumentException("Device info cannot be empty.", nameof(deviceInfo));
        if (string.IsNullOrWhiteSpace(ipAddress))
            throw new ArgumentException("IP address cannot be empty.", nameof(ipAddress));

        var now = DateTime.UtcNow;

        return new UserSession
        {
            Id = id,
            UserId = userId,
            DeviceInfo = deviceInfo.Trim(),
            IpAddress = ipAddress.Trim(),
            LastActiveAt = now,
            CreatedAt = now,
            ExpiresAt = expiresAt,
            IsActive = true,
            RefreshTokenId = refreshTokenId
        };
    }

    public void Expire()
    {
        IsActive = false;
        LastActiveAt = DateTime.UtcNow;
    }

    public void UpdateActivity()
    {
        if (!IsActive)
            throw new InvalidOperationException("Cannot update activity for an expired session.");

        LastActiveAt = DateTime.UtcNow;
    }

    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
}
