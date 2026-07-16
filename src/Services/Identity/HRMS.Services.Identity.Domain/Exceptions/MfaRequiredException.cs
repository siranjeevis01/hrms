namespace HRMS.Services.Identity.Domain.Exceptions;

public class MfaRequiredException : Exception
{
    public Guid UserId { get; }

    public MfaRequiredException(Guid userId)
        : base("Multi-factor authentication is required to complete sign-in.")
    {
        UserId = userId;
    }

    public MfaRequiredException(string message)
        : base(message) { }

    public MfaRequiredException(string message, Exception innerException)
        : base(message, innerException) { }
}
