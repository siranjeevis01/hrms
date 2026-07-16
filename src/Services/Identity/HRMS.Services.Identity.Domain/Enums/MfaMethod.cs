namespace HRMS.Services.Identity.Domain.Enums;

public enum MfaMethod
{
    TOTP = 0,
    SMS = 1,
    Email = 2,
    Phone = 3
}
