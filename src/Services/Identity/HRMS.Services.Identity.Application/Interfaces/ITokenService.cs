namespace HRMS.Services.Identity.Application.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(Guid userId, string email, IReadOnlyList<string> roles, IReadOnlyList<string> permissions, Guid? tenantId);
    string GenerateRefreshToken();
    string GenerateEmailVerificationToken();
    string GeneratePasswordResetToken();
    DateTime GetAccessTokenExpiration();
}
