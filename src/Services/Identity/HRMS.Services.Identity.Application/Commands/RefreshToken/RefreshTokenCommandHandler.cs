using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.DTOs;
using HRMS.Services.Identity.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<AuthResponseDto>>
{
    private readonly IIdentityDbContext _context;
    private readonly ITokenService _tokenService;

    public RefreshTokenCommandHandler(
        IIdentityDbContext context,
        ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<Result<AuthResponseDto>> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var storedToken = await _context.FindRefreshTokenAsync(request.RefreshToken, cancellationToken);

        if (storedToken is null)
        {
            return Result<AuthResponseDto>.Failure(
                Error.Unauthorized("Auth.InvalidRefreshToken", "Invalid refresh token."));
        }

        if (storedToken.IsExpired)
        {
            return Result<AuthResponseDto>.Failure(
                Error.Unauthorized("Auth.RefreshTokenExpired", "Refresh token has expired."));
        }

        if (storedToken.Revoked.HasValue)
        {
            return Result<AuthResponseDto>.Failure(
                Error.Unauthorized("Auth.RefreshTokenRevoked", "Refresh token has been revoked."));
        }

        var newRefreshTokenValue = _tokenService.GenerateRefreshToken();
        var rotated = storedToken.Rotate(newRefreshTokenValue, request.IpAddress);
        _context.AddRefreshToken(rotated);

        var user = await _context.FindUserByIdAsync(storedToken.UserId, cancellationToken);
        if (user is null)
        {
            return Result<AuthResponseDto>.Failure(
                Error.NotFound("Auth.UserNotFound", "User associated with this token no longer exists."));
        }

        var roles = await _context.GetUserRoleNamesAsync(user.Id, cancellationToken);
        var permissions = await _context.GetUserPermissionsAsync(user.Id, cancellationToken);

        var accessToken = _tokenService.GenerateAccessToken(
            user.Id,
            user.Email,
            roles,
            permissions,
            user.TenantId);

        await _context.SaveChangesAsync(cancellationToken);

        var dto = new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshTokenValue,
            ExpiresIn = (int)(_tokenService.GetAccessTokenExpiration() - DateTime.UtcNow).TotalSeconds,
            TokenType = "Bearer",
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                ProfilePictureUrl = user.ProfilePictureUrl,
                IsMfaEnabled = user.IsMfaEnabled,
                Roles = roles,
                Permissions = permissions,
                CreatedAt = user.CreatedAt
            }
        };

        return Result<AuthResponseDto>.Success(dto);
    }
}
