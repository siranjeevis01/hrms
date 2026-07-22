using System.Security.Claims;
using HRMS.Services.Identity.Application.Commands.ChangePassword;
using HRMS.Services.Identity.Application.Commands.ConfirmResetPassword;
using HRMS.Services.Identity.Application.Commands.EnableMfa;
using HRMS.Services.Identity.Application.Commands.Login;
using HRMS.Services.Identity.Application.Commands.Logout;
using HRMS.Services.Identity.Application.Commands.RefreshToken;
using HRMS.Services.Identity.Application.Commands.Register;
using HRMS.Services.Identity.Application.Commands.ResetPassword;
using HRMS.Services.Identity.Application.Commands.RevokeToken;
using HRMS.Services.Identity.Application.Commands.VerifyEmail;
using HRMS.Services.Identity.Application.Commands.VerifyMfa;
using HRMS.Services.Identity.Application.DTOs;
using HRMS.Services.Identity.Application.Interfaces;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Identity.API.Controllers;

[ApiController]
[Route("api/identity/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuthController> _logger;
    private readonly HRMS.Services.Identity.Infrastructure.Services.IFirebaseAuthService _firebaseAuthService;
    private readonly IIdentityDbContext _identityDbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthController(
        IMediator mediator,
        ILogger<AuthController> logger,
        HRMS.Services.Identity.Infrastructure.Services.IFirebaseAuthService firebaseAuthService,
        IIdentityDbContext identityDbContext,
        IPasswordHasher passwordHasher,
        ITokenService tokenService)
    {
        _mediator = mediator;
        _logger = logger;
        _firebaseAuthService = firebaseAuthService;
        _identityDbContext = identityDbContext;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registration attempt for email: {Email}", command.Email);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new ApiErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.Message,
                Details = result.Error.Code
            });

        return CreatedAtAction(nameof(Register), result.Value);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Login attempt for email: {Email}", request.Email);

        var command = new LoginCommand(
            request.Email,
            request.Password,
            HttpContext.Connection.RemoteIpAddress?.ToString(),
            Request.Headers.UserAgent.ToString(),
            request.DeviceInfo);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return Unauthorized(new ApiErrorResponse
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Message = result.Error.Message,
                Details = result.Error.Code
            });

        return Ok(result.Value);
    }

    [HttpPost("refresh")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshToken(
        [FromBody] RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RefreshTokenCommand(
            request.RefreshToken,
            HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            Request.Headers.UserAgent.ToString());

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return Unauthorized(new ApiErrorResponse
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Message = result.Error.Message,
                Details = result.Error.Code
            });

        return Ok(result.Value);
    }

    [HttpPost("revoke-token")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RevokeToken(
        [FromBody] RevokeTokenRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RevokeTokenCommand(
            request.RefreshToken,
            HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown");

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new ApiErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.Message,
                Details = result.Error.Code
            });

        return NoContent();
    }

    [HttpPost("logout")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Logout(
        [FromBody] LogoutRequest request,
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        if (userId is null)
            return Unauthorized();

        var command = new LogoutCommand(userId.Value, request.SessionId);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new ApiErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.Message,
                Details = result.Error.Code
            });

        return NoContent();
    }

    [HttpPost("forgot-password")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ForgotPassword(
        [FromBody] ForgotPasswordRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Password reset requested for email: {Email}", request.Email);

        var command = new ResetPasswordCommand(request.Email);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new ApiErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.Message,
                Details = result.Error.Code
            });

        return Accepted(new { message = "If the email exists, a password reset link has been sent." });
    }

    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword(
        [FromBody] ResetPasswordConfirmRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ConfirmResetPasswordCommand(request.Token, request.NewPassword);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new ApiErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.Message,
                Details = result.Error.Code
            });

        return Ok(new { message = "Password has been reset successfully." });
    }

    [HttpPost("verify-email")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> VerifyEmail(
        [FromBody] VerifyEmailRequest request,
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        if (userId is null)
            return Unauthorized();

        var command = new VerifyEmailCommand(userId.Value, request.Token);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new ApiErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.Message,
                Details = result.Error.Code
            });

        return Ok(new { message = "Email verified successfully." });
    }

    [HttpPost("enable-mfa")]
    [Authorize]
    [ProducesResponseType(typeof(MfaSetupDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EnableMfa(
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        if (userId is null)
            return Unauthorized();

        var command = new EnableMfaCommand(userId.Value);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new ApiErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.Message,
                Details = result.Error.Code
            });

        return Ok(result.Value);
    }

    [HttpPost("verify-mfa")]
    [Authorize]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> VerifyMfa(
        [FromBody] VerifyMfaRequest request,
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        if (userId is null)
            return Unauthorized();

        var command = new VerifyMfaCommand(userId.Value, request.Code);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new ApiErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.Message,
                Details = result.Error.Code
            });

        return Ok(new { verified = result.Value });
    }

    [HttpPost("firebase")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> FirebaseLogin(
        [FromBody] FirebaseLoginRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Firebase login attempt");

        try
        {
            await _firebaseAuthService.InitializeAppAsync();

            var firebaseToken = await _firebaseAuthService.VerifyIdTokenAsync(request.IdToken);
            if (firebaseToken == null)
            {
                return Unauthorized(new ApiErrorResponse
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = "Invalid or expired Firebase token.",
                    Details = "firebase_token_invalid"
                });
            }

            var email = firebaseToken.Claims.TryGetValue("email", out var emailClaim)
                ? emailClaim?.ToString() : null;
            var name = firebaseToken.Claims.TryGetValue("name", out var nameClaim)
                ? nameClaim?.ToString() : null;

            if (string.IsNullOrWhiteSpace(email))
            {
                return Unauthorized(new ApiErrorResponse
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = "No email found in Firebase token.",
                    Details = "firebase_no_email"
                });
            }

            var user = await _identityDbContext.FindUserByEmailAsync(email, cancellationToken);

            if (user == null)
            {
                var firstName = name?.Split(' ')[0] ?? email.Split('@')[0];
                var lastName = name?.Contains(' ') == true ? name.Split(' ').Last() : "User";

                user = Domain.Entities.ApplicationUser.Create(
                    Guid.NewGuid(), email, firstName, lastName);
                user.SetFirebaseUid(firebaseToken.Uid);
                user.VerifyEmail();

                _identityDbContext.AddUser(user);

                var refreshToken = Domain.Entities.RefreshToken.Create(
                    Guid.NewGuid(),
                    _tokenService.GenerateRefreshToken(),
                    user.Id,
                    DateTime.UtcNow.AddDays(7),
                    "firebase_login");

                _identityDbContext.AddRefreshToken(refreshToken);

                await _identityDbContext.SetUserPasswordHashAsync(
                    user.Id,
                    _passwordHasher.HashPassword(Guid.NewGuid().ToString("N")),
                    cancellationToken);

                await _identityDbContext.SaveChangesAsync(cancellationToken);

                var roles = await _identityDbContext.GetUserRoleNamesAsync(user.Id, cancellationToken);
                var permissions = await _identityDbContext.GetUserPermissionsAsync(user.Id, cancellationToken);

                var accessToken = _tokenService.GenerateAccessToken(
                    user.Id, user.Email, roles, permissions, user.TenantId);

                return Ok(new AuthResponseDto
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
                });
            }

            if (!user.IsActive)
            {
                return Unauthorized(new ApiErrorResponse
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = "Account has been deactivated.",
                    Details = "account_deactivated"
                });
            }

            user.UpdateLastLogin(HttpContext.Connection.RemoteIpAddress?.ToString());
            _identityDbContext.UpdateUser(user);

            var existingRoles = await _identityDbContext.GetUserRoleNamesAsync(user.Id, cancellationToken);
            var existingPermissions = await _identityDbContext.GetUserPermissionsAsync(user.Id, cancellationToken);

            var newRefreshToken = Domain.Entities.RefreshToken.Create(
                Guid.NewGuid(),
                _tokenService.GenerateRefreshToken(),
                user.Id,
                DateTime.UtcNow.AddDays(7),
                "firebase_login");

            _identityDbContext.AddRefreshToken(newRefreshToken);
            await _identityDbContext.SaveChangesAsync(cancellationToken);

            var token = _tokenService.GenerateAccessToken(
                user.Id, user.Email, existingRoles, existingPermissions, user.TenantId);

            return Ok(new AuthResponseDto
            {
                AccessToken = token,
                RefreshToken = newRefreshToken.Token,
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
                    Roles = existingRoles,
                    Permissions = existingPermissions,
                    CreatedAt = user.CreatedAt
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Firebase login failed");
            return Unauthorized(new ApiErrorResponse
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Message = "Firebase authentication failed.",
                Details = ex.Message
            });
        }
    }

    [HttpPost("change-password")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword(
        [FromBody] ChangePasswordRequest request,
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        if (userId is null)
            return Unauthorized();

        var command = new ChangePasswordCommand(userId.Value, request.OldPassword, request.NewPassword);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new ApiErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.Message,
                Details = result.Error.Code
            });

        return NoContent();
    }

    private Guid? GetUserId()
    {
        var sub = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                  ?? User.FindFirst("sub")?.Value;
        return sub is not null && Guid.TryParse(sub, out var userId) ? userId : null;
    }
}

#region Request DTOs

public record RegisterRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string? PhoneNumber,
    Guid? TenantId);

public record LoginRequest(
    string Email,
    string Password,
    string? DeviceInfo);

public record RefreshTokenRequest(string RefreshToken);

public record RevokeTokenRequest(string RefreshToken);

public record LogoutRequest(Guid SessionId);

public record ForgotPasswordRequest(string Email);

public record ResetPasswordConfirmRequest(string Token, string NewPassword);

public record VerifyEmailRequest(string Token);

public record VerifyMfaRequest(string Code);

public record ChangePasswordRequest(string OldPassword, string NewPassword);

public record FirebaseLoginRequest(string IdToken);

#endregion
