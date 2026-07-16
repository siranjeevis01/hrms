using System.Text.Json;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using HRMS.Services.Notification.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HRMS.Services.Notification.Infrastructure.Providers;

public class FirebasePushOptions
{
    public string ProjectId { get; set; } = string.Empty;
    public string ServiceAccountKeyPath { get; set; } = string.Empty;
}

public class FirebasePushProvider : IPushProvider
{
    private readonly FirebasePushOptions _options;
    private readonly ILogger<FirebasePushProvider> _logger;
    private FirebaseApp? _app;

    public FirebasePushProvider(IOptions<FirebasePushOptions> options, ILogger<FirebasePushProvider> logger)
    {
        _options = options.Value;
        _logger = logger;

        if (_app == null && !string.IsNullOrEmpty(_options.ProjectId))
        {
            try
            {
                _app = FirebaseApp.Create(new AppOptions
                {
                    Credential = string.IsNullOrEmpty(_options.ServiceAccountKeyPath)
                        ? GoogleCredential.GetApplicationDefault()
                        : GoogleCredential.FromFile(_options.ServiceAccountKeyPath),
                    ProjectId = _options.ProjectId
                });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to initialize Firebase. Push notifications will be disabled.");
            }
        }
    }

    public async Task<PushSendResult> SendAsync(
        string userId, string title, string body,
        Dictionary<string, string>? data = null,
        CancellationToken cancellationToken = default)
    {
        if (_app == null)
            return PushSendResult.Failure("Firebase not initialized");

        try
        {
            var message = new Message
            {
                Topic = $"user_{userId}",
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = title,
                    Body = body
                },
                Data = data?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
            };

            var response = await FirebaseMessaging.DefaultInstance.SendAsync(message, cancellationToken);
            _logger.LogInformation("Push sent to user {UserId}. MessageId: {MessageId}", userId, response);
            return PushSendResult.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception sending push to user {UserId}", userId);
            return PushSendResult.Failure(ex.Message);
        }
    }
}
