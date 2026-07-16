namespace HRMS.Services.Identity.Application.Interfaces;

public interface IEmailService
{
    Task SendEmailVerificationAsync(string toEmail, string firstName, string token, CancellationToken cancellationToken = default);
    Task SendPasswordResetAsync(string toEmail, string firstName, string token, CancellationToken cancellationToken = default);
    Task SendPasswordChangedNotificationAsync(string toEmail, string firstName, CancellationToken cancellationToken = default);
}
