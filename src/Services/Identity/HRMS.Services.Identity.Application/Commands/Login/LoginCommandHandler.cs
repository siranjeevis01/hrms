using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.DTOs;
using HRMS.Services.Identity.Application.Events;
using HRMS.Services.Identity.Application.Interfaces;
using HRMS.Services.Identity.Domain.Entities;
using HRMS.Services.Identity.Domain.Exceptions;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthResponseDto>>
{
    private readonly IIdentityDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IPublisher _publisher;

    public LoginCommandHandler(
        IIdentityDbContext context,
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        IPublisher publisher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _publisher = publisher;
    }

    public async Task<Result<AuthResponseDto>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.FindUserByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            return Result<AuthResponseDto>.Failure(
                Error.Unauthorized("Auth.InvalidCredentials", "Invalid email or password."));
        }

        if (!user.IsActive)
        {
            return Result<AuthResponseDto>.Failure(
                Error.Forbidden("Auth.AccountDeactivated", "This account has been deactivated."));
        }

        if (!user.IsEmailVerified)
        {
            return Result<AuthResponseDto>.Failure(
                Error.Forbidden("Auth.EmailNotVerified", "Please verify your email address before logging in."));
        }

        var storedHash = await _context.GetUserPasswordHashAsync(user.Id, cancellationToken);

        if (storedHash is null || !_passwordHasher.VerifyPassword(storedHash, request.Password))
        {
            _context.AddAuditLog(AuditLog.Create(
                Guid.NewGuid(),
                "LoginFailed",
                user.Id,
                request.IpAddress,
                request.UserAgent,
                $"Failed login attempt for {request.Email}",
                false,
                "Invalid password"));

            await _context.SaveChangesAsync(cancellationToken);

            return Result<AuthResponseDto>.Failure(
                Error.Unauthorized("Auth.InvalidCredentials", "Invalid email or password."));
        }

        if (user.IsMfaEnabled)
        {
            return Result<AuthResponseDto>.Failure(
                Error.Failure("Auth.MfaRequired", "Multi-factor authentication is required."));
        }

        user.UpdateLastLogin(request.IpAddress);

        var roles = await _context.GetUserRoleNamesAsync(user.Id, cancellationToken);
        var permissions = await _context.GetUserPermissionsAsync(user.Id, cancellationToken);

        var refreshToken = Domain.Entities.RefreshToken.Create(
            Guid.NewGuid(),
            _tokenService.GenerateRefreshToken(),
            user.Id,
            DateTime.UtcNow.AddDays(7),
            request.IpAddress ?? "unknown");

        _context.AddRefreshToken(refreshToken);

        var session = UserSession.Create(
            Guid.NewGuid(),
            user.Id,
            request.DeviceInfo ?? "Unknown Device",
            request.IpAddress ?? "unknown",
            DateTime.UtcNow.AddDays(7),
            refreshToken.Id);

        _context.AddUserSession(session);

        _context.AddAuditLog(AuditLog.Create(
            Guid.NewGuid(),
            "LoginSuccess",
            user.Id,
            request.IpAddress,
            request.UserAgent,
            $"User {request.Email} logged in successfully"));

        await _context.SaveChangesAsync(cancellationToken);

        var accessToken = _tokenService.GenerateAccessToken(
            user.Id,
            user.Email,
            roles,
            permissions,
            user.TenantId);

        var dto = new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
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

        await _publisher.Publish(
            new UserLoggedInEvent(user.Id, user.Email, request.IpAddress, "Email"),
            cancellationToken);

        return Result<AuthResponseDto>.Success(dto);
    }
}
