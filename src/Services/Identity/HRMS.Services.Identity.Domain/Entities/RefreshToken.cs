namespace HRMS.Services.Identity.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; private set; }
    public string Token { get; private set; } = string.Empty;
    public Guid UserId { get; private set; }
    public DateTime Expires { get; private set; }
    public DateTime Created { get; private set; }
    public string CreatedByIp { get; private set; } = string.Empty;
    public DateTime? Revoked { get; private set; }
    public string? RevokedByIp { get; private set; }
    public string? ReplacedByToken { get; private set; }

    public bool IsExpired => DateTime.UtcNow >= Expires;
    public bool IsActive => Revoked == null && !IsExpired;

    private RefreshToken() { }

    public static RefreshToken Create(
        Guid id,
        string token,
        Guid userId,
        DateTime expires,
        string createdByIp)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Refresh token ID cannot be empty.", nameof(id));
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Token cannot be empty.", nameof(token));
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));
        if (string.IsNullOrWhiteSpace(createdByIp))
            throw new ArgumentException("Created by IP cannot be empty.", nameof(createdByIp));

        return new RefreshToken
        {
            Id = id,
            Token = token,
            UserId = userId,
            Expires = expires,
            Created = DateTime.UtcNow,
            CreatedByIp = createdByIp
        };
    }

    public void Revoke(string revokedByIp, string? replacedByToken = null)
    {
        Revoked = DateTime.UtcNow;
        RevokedByIp = revokedByIp;
        ReplacedByToken = replacedByToken;
    }

    public RefreshToken Rotate(string newToken, string ipAddress)
    {
        Revoke(ipAddress, newToken);

        return Create(
            Guid.NewGuid(),
            newToken,
            UserId,
            Expires,
            ipAddress);
    }
}
