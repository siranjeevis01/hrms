using Microsoft.Extensions.Logging;

namespace HRMS.Services.Identity.Infrastructure.Services;

public class NoOpEmailService : HRMS.Services.Identity.Application.Interfaces.IEmailService
{
    private readonly ILogger<NoOpEmailService> _logger;

    public NoOpEmailService(ILogger<NoOpEmailService> logger)
    {
        _logger = logger;
    }

    public Task SendEmailVerificationAsync(string toEmail, string firstName, string token, CancellationToken cancellationToken = default)
    {
        _logger.LogWarning("Email sending not configured. Verification email for {Email} was not sent. Token: {Token}", toEmail, token);
        return Task.CompletedTask;
    }

    public Task SendPasswordResetAsync(string toEmail, string firstName, string token, CancellationToken cancellationToken = default)
    {
        _logger.LogWarning("Email sending not configured. Password reset email for {Email} was not sent. Token: {Token}", toEmail, token);
        return Task.CompletedTask;
    }

    public Task SendPasswordChangedNotificationAsync(string toEmail, string firstName, CancellationToken cancellationToken = default)
    {
        _logger.LogWarning("Email sending not configured. Password changed notification for {Email} was not sent.", toEmail);
        return Task.CompletedTask;
    }
}
