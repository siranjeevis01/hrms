using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.DTOs;
using HRMS.Services.Identity.Application.Events;
using HRMS.Services.Identity.Application.Interfaces;
using HRMS.Services.Identity.Domain.Entities;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<AuthResponseDto>>
{
    private readonly IIdentityDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    private readonly IPublisher _publisher;

    public RegisterUserCommandHandler(
        IIdentityDbContext context,
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        IEmailService emailService,
        IPublisher publisher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _emailService = emailService;
        _publisher = publisher;
    }

    public async Task<Result<AuthResponseDto>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        if (await _context.EmailExistsAsync(request.Email, cancellationToken))
        {
            return Result<AuthResponseDto>.Failure(
                Error.Conflict("Auth.EmailAlreadyExists", "A user with this email address already exists."));
        }

        var userId = Guid.NewGuid();
        var hashedPassword = _passwordHasher.HashPassword(request.Password);

        var user = ApplicationUser.Create(
            userId,
            request.Email,
            request.FirstName,
            request.LastName,
            request.TenantId);

        user.SetFirebaseUid(Guid.NewGuid().ToString("N"));
        user.VerifyEmail();

        _context.AddUser(user);

        var refreshToken = Domain.Entities.RefreshToken.Create(
            Guid.NewGuid(),
            _tokenService.GenerateRefreshToken(),
            userId,
            DateTime.UtcNow.AddDays(7),
            "registration");

        _context.AddRefreshToken(refreshToken);

        await _context.SetUserPasswordHashAsync(userId, hashedPassword, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        var roles = await _context.GetUserRoleNamesAsync(userId, cancellationToken);
        var permissions = await _context.GetUserPermissionsAsync(userId, cancellationToken);

        var accessToken = _tokenService.GenerateAccessToken(
            userId,
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
                Id = userId,
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
            new UserRegisteredEvent(userId, user.Email, user.FirstName, user.LastName, user.TenantId),
            cancellationToken);

        var verificationToken = _tokenService.GeneratePasswordResetToken();
        await _emailService.SendEmailVerificationAsync(
            user.Email,
            user.FirstName,
            verificationToken,
            cancellationToken);

        return Result<AuthResponseDto>.Success(dto);
    }
}
