namespace HRMS.Services.Identity.Domain.Entities;

public class ApplicationUser
{
    public Guid Id { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string? PhoneNumber { get; private set; }
    public string? ProfilePictureUrl { get; private set; }
    public string? FirebaseUid { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsEmailVerified { get; private set; }
    public bool IsPhoneVerified { get; private set; }
    public bool IsMfaEnabled { get; private set; }
    public string? MfaSecret { get; private set; }
    public string? PasswordHash { get; private set; }
    public DateTime? LastLoginAt { get; private set; }
    public string? LastLoginIp { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public Guid? TenantId { get; private set; }

    private readonly List<UserRole> _userRoles = new();
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

    private readonly List<UserPermission> _userPermissions = new();
    public IReadOnlyCollection<UserPermission> UserPermissions => _userPermissions.AsReadOnly();

    private ApplicationUser() { }

    public static ApplicationUser Create(
        Guid id,
        string email,
        string firstName,
        string lastName,
        Guid? tenantId = null)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty.", nameof(id));
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.", nameof(email));
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty.", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty.", nameof(lastName));

        var now = DateTime.UtcNow;

        return new ApplicationUser
        {
            Id = id,
            Email = email.Trim().ToLowerInvariant(),
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            TenantId = tenantId,
            IsActive = true,
            IsEmailVerified = false,
            IsPhoneVerified = false,
            IsMfaEnabled = false,
            CreatedAt = now,
            UpdatedAt = now
        };
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void EnableMfa(string secret)
    {
        if (string.IsNullOrWhiteSpace(secret))
            throw new ArgumentException("MFA secret cannot be empty.", nameof(secret));

        IsMfaEnabled = true;
        MfaSecret = secret;
        UpdatedAt = DateTime.UtcNow;
    }

    public void DisableMfa()
    {
        IsMfaEnabled = false;
        MfaSecret = null;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateLastLogin(string? ipAddress)
    {
        LastLoginAt = DateTime.UtcNow;
        LastLoginIp = ipAddress;
        UpdatedAt = DateTime.UtcNow;
    }

    public void VerifyEmail()
    {
        IsEmailVerified = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void VerifyPhone()
    {
        IsPhoneVerified = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateProfile(string firstName, string lastName, string? phoneNumber, string? profilePictureUrl)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty.", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty.", nameof(lastName));

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        PhoneNumber = phoneNumber?.Trim();
        ProfilePictureUrl = profilePictureUrl?.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetFirebaseUid(string firebaseUid)
    {
        if (string.IsNullOrWhiteSpace(firebaseUid))
            throw new ArgumentException("Firebase UID cannot be empty.", nameof(firebaseUid));

        FirebaseUid = firebaseUid;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetPasswordHash(string hashedPassword)
    {
        PasswordHash = hashedPassword;
        UpdatedAt = DateTime.UtcNow;
    }

    public string GetFullName() => $"{FirstName} {LastName}";
}
