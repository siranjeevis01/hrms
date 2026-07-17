using HRMS.Services.Identity.Domain.Entities;
using Microsoft.Extensions.Options;

namespace HRMS.Services.Identity.Infrastructure.Services;

public class ApplicationTokenServiceAdapter : HRMS.Services.Identity.Application.Interfaces.ITokenService
{
    private readonly TokenService _tokenService;
    private readonly JwtSettings _jwtSettings;

    public ApplicationTokenServiceAdapter(TokenService tokenService, IOptions<JwtSettings> jwtSettings)
    {
        _tokenService = tokenService;
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateAccessToken(Guid userId, string email, IReadOnlyList<string> roles, IReadOnlyList<string> permissions, Guid? tenantId)
    {
        var user = ApplicationUser.Create(userId, email, email, email, tenantId);
        return _tokenService.CreateAccessToken(user, roles.ToList(), permissions.ToList());
    }

    public string GenerateRefreshToken() => _tokenService.CreateRefreshToken();

    public string GenerateEmailVerificationToken() => _tokenService.CreateRefreshToken();

    public string GeneratePasswordResetToken() => _tokenService.CreateRefreshToken();

    public DateTime GetAccessTokenExpiration() => DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes);
}
