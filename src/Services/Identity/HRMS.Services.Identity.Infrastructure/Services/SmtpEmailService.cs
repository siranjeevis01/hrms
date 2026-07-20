using System.Net;
using System.Net.Mail;
using HRMS.Services.Identity.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HRMS.Services.Identity.Infrastructure.Services;

public sealed class SmtpEmailService : IEmailService
{
    private readonly SmtpOptions _options;
    private readonly ILogger<SmtpEmailService> _logger;

    public SmtpEmailService(IOptions<SmtpOptions> options, ILogger<SmtpEmailService> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public Task SendEmailVerificationAsync(string toEmail, string firstName, string token, CancellationToken cancellationToken = default)
    {
        var verifyUrl = $"https://hrms-pro.netlify.app/auth/verify-email?token={Uri.EscapeDataString(token)}";
        var body = $"""
            <div style="font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;">
                <h2 style="color: #1e40af;">Welcome to HRMS Pro!</h2>
                <p>Hi {firstName},</p>
                <p>Thank you for registering. Please verify your email address by clicking the button below:</p>
                <div style="text-align: center; margin: 30px 0;">
                    <a href="{verifyUrl}" style="background-color: #1e40af; color: white; padding: 12px 24px; text-decoration: none; border-radius: 6px; font-weight: bold;">Verify Email Address</a>
                </div>
                <p style="color: #666; font-size: 14px;">If the button doesn't work, copy and paste this link into your browser:</p>
                <p style="color: #666; font-size: 14px; word-break: break-all;">{verifyUrl}</p>
                <p style="color: #666; font-size: 14px;">This link will expire in 24 hours.</p>
                <hr style="border: none; border-top: 1px solid #eee; margin: 20px 0;" />
                <p style="color: #999; font-size: 12px;">If you did not create an account, you can safely ignore this email.</p>
            </div>
            """;

        return SendAsync(toEmail, "Verify your HRMS Pro email address", body, true, cancellationToken);
    }

    public Task SendPasswordResetAsync(string toEmail, string firstName, string token, CancellationToken cancellationToken = default)
    {
        var resetUrl = $"https://hrms-pro.netlify.app/auth/reset-password?token={Uri.EscapeDataString(token)}";
        var body = $"""
            <div style="font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;">
                <h2 style="color: #1e40af;">Password Reset Request</h2>
                <p>Hi {firstName},</p>
                <p>We received a request to reset your password. Click the button below to create a new password:</p>
                <div style="text-align: center; margin: 30px 0;">
                    <a href="{resetUrl}" style="background-color: #dc2626; color: white; padding: 12px 24px; text-decoration: none; border-radius: 6px; font-weight: bold;">Reset Password</a>
                </div>
                <p style="color: #666; font-size: 14px;">If the button doesn't work, copy and paste this link into your browser:</p>
                <p style="color: #666; font-size: 14px; word-break: break-all;">{resetUrl}</p>
                <p style="color: #666; font-size: 14px;">This link will expire in 1 hour.</p>
                <p style="color: #dc2626; font-size: 14px;"><strong>If you did not request a password reset, your account may be compromised. Please change your password immediately.</strong></p>
                <hr style="border: none; border-top: 1px solid #eee; margin: 20px 0;" />
                <p style="color: #999; font-size: 12px;">This email was sent from HRMS Pro.</p>
            </div>
            """;

        return SendAsync(toEmail, "HRMS Pro - Password Reset Request", body, true, cancellationToken);
    }

    public Task SendPasswordChangedNotificationAsync(string toEmail, string firstName, CancellationToken cancellationToken = default)
    {
        var body = $"""
            <div style="font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;">
                <h2 style="color: #1e40af;">Password Changed</h2>
                <p>Hi {firstName},</p>
                <p>Your password has been successfully changed.</p>
                <p style="color: #666; font-size: 14px;">If you did not make this change, please contact your administrator immediately.</p>
                <hr style="border: none; border-top: 1px solid #eee; margin: 20px 0;" />
                <p style="color: #999; font-size: 12px;">This email was sent from HRMS Pro.</p>
            </div>
            """;

        return SendAsync(toEmail, "HRMS Pro - Password Changed", body, true, cancellationToken);
    }

    private async Task SendAsync(string toEmail, string subject, string body, bool isHtml, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_options.Host) || _options.Host == "localhost" && string.IsNullOrWhiteSpace(_options.UserName))
        {
            _logger.LogWarning("SMTP not configured. Email to {Email} was not sent. Subject: {Subject}. Body: {Body}", toEmail, subject, body);
            return;
        }

        try
        {
            using var message = new MailMessage();
            message.From = new MailAddress(_options.FromEmail, _options.FromName);
            message.To.Add(toEmail);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = isHtml;

            using var client = new SmtpClient(_options.Host, _options.Port)
            {
                EnableSsl = _options.EnableSsl,
                Credentials = string.IsNullOrWhiteSpace(_options.UserName)
                    ? CredentialCache.DefaultNetworkCredentials
                    : new NetworkCredential(_options.UserName, _options.Password),
                Timeout = 30000
            };

            await client.SendMailAsync(message, cancellationToken);

            _logger.LogInformation("Email sent to {Email}. Subject: {Subject}", toEmail, subject);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {Email}. Subject: {Subject}", toEmail, subject);
        }
    }
}
