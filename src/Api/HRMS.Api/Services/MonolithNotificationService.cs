using System.Net;
using System.Net.Mail;
using HRMS.Shared.Kernel.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HRMS.Api.Services;

public sealed class SmtpSettings
{
    public const string SectionName = "Smtp";
    public string Host { get; set; } = "localhost";
    public int Port { get; set; } = 587;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FromEmail { get; set; } = "noreply@hrmspro.com";
    public string FromName { get; set; } = "HRMS Pro";
    public bool EnableSsl { get; set; } = true;
}

public sealed class MonolithNotificationService : INotificationService
{
    private readonly SmtpSettings _smtp;
    private readonly ILogger<MonolithNotificationService> _logger;

    public MonolithNotificationService(IOptions<SmtpSettings> smtp, ILogger<MonolithNotificationService> logger)
    {
        _smtp = smtp.Value;
        _logger = logger;
    }

    public Task SendEmailAsync(string to, string subject, string body, bool isHtml = false, CancellationToken cancellationToken = default)
    {
        return SendSmtpEmailAsync(new[] { to }, subject, body, isHtml, cancellationToken);
    }

    public Task SendEmailAsync(IReadOnlyList<string> to, string subject, string body, bool isHtml = false, CancellationToken cancellationToken = default)
    {
        return SendSmtpEmailAsync(to, subject, body, isHtml, cancellationToken);
    }

    public Task SendPushNotificationAsync(Guid userId, string title, string body, Dictionary<string, string>? data = null, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Push notification for {UserId}: {Title}. Push delivery requires the Notification microservice with Firebase configured.", userId, title);
        return Task.CompletedTask;
    }

    public Task SendInAppNotificationAsync(Guid userId, string title, string message, string? actionUrl = null, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("In-app notification for {UserId}: {Title} - {Message}. Real-time delivery requires SignalR hub connection.", userId, title, message);
        return Task.CompletedTask;
    }

    private async Task SendSmtpEmailAsync(IReadOnlyList<string> recipients, string subject, string body, bool isHtml, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_smtp.Host) || (_smtp.Host == "localhost" && string.IsNullOrWhiteSpace(_smtp.UserName)))
        {
            _logger.LogWarning("SMTP not configured. Email to {Recipients} was not sent. Subject: {Subject}", string.Join(", ", recipients), subject);
            return;
        }

        try
        {
            using var message = new MailMessage();
            message.From = new MailAddress(_smtp.FromEmail, _smtp.FromName);
            foreach (var recipient in recipients)
            {
                if (!string.IsNullOrWhiteSpace(recipient))
                    message.To.Add(recipient);
            }

            if (message.To.Count == 0)
            {
                _logger.LogWarning("No valid recipients for email. Subject: {Subject}", subject);
                return;
            }

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = isHtml;

            using var client = new SmtpClient(_smtp.Host, _smtp.Port)
            {
                EnableSsl = _smtp.EnableSsl,
                Credentials = string.IsNullOrWhiteSpace(_smtp.UserName)
                    ? CredentialCache.DefaultNetworkCredentials
                    : new NetworkCredential(_smtp.UserName, _smtp.Password),
                Timeout = 30000
            };

            await client.SendMailAsync(message, cancellationToken);

            _logger.LogInformation("Email sent to {Recipients}. Subject: {Subject}", string.Join(", ", message.To), subject);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {Recipients}. Subject: {Subject}", string.Join(", ", recipients), subject);
        }
    }
}
