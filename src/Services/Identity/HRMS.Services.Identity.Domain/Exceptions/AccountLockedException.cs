namespace HRMS.Services.Identity.Domain.Exceptions;

public class AccountLockedException : Exception
{
    public DateTime? LockedUntil { get; }

    public AccountLockedException()
        : base("Account has been locked due to too many failed login attempts.") { }

    public AccountLockedException(DateTime lockedUntil)
        : base($"Account has been locked until {lockedUntil:u}.")
    {
        LockedUntil = lockedUntil;
    }

    public AccountLockedException(string message)
        : base(message) { }

    public AccountLockedException(string message, Exception innerException)
        : base(message, innerException) { }
}
