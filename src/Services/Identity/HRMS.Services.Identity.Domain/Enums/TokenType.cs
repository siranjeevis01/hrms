namespace HRMS.Services.Identity.Domain.Enums;

public enum TokenType
{
    Access = 0,
    Refresh = 1,
    EmailVerification = 2,
    PasswordReset = 3,
    Mfa = 4
}
