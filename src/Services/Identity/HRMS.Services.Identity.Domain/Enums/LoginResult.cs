namespace HRMS.Services.Identity.Domain.Enums;

public enum LoginResult
{
    Success = 0,
    InvalidCredentials = 1,
    AccountLocked = 2,
    MfaRequired = 3,
    EmailNotVerified = 4,
    AccountDeactivated = 5,
    TooManyAttempts = 6
}
