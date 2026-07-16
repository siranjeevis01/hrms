using System.Text.Json;
using System.Text;
using HRMS.Services.Notification.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;

namespace HRMS.Services.Notification.Infrastructure.Providers;

public class BrevoEmailOptions
{
    public string ApiKey { get; set; } = string.Empty;
    public string FromEmail { get; set; } = string.Empty;
    public string FromName { get; set; } = "HRMS Pro";
    public string BaseUrl { get; set; } = "https://api.sendinblue.com/v3";
}

public class BrevoEmailProvider : IEmailProvider
{
    private readonly HttpClient _httpClient;
    private readonly BrevoEmailOptions _options;
    private readonly ILogger<BrevoEmailProvider> _logger;
    private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;

    public BrevoEmailProvider(
        HttpClient httpClient,
        IOptions<BrevoEmailOptions> options,
        ILogger<BrevoEmailProvider> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;

        _retryPolicy = Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                onRetry: (outcome, delay, attempt, context) =>
                {
                    _logger.LogWarning("Retry {Attempt} for email after {Delay}s. Status: {Status}",
                        attempt, delay.TotalSeconds, outcome.Result?.StatusCode);
                });
    }

    public async Task<EmailSendResult> SendAsync(
        string to, string subject, string body, bool isHtml,
        string? cc = null, string? bcc = null, string? attachments = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var payload = new
            {
                sender = new { name = _options.FromName, email = _options.FromEmail },
                to = to.Split(',', ';').Select(e => new { email = e.Trim() }).ToArray(),
                cc = !string.IsNullOrEmpty(cc) ? cc.Split(',', ';').Select(e => new { email = e.Trim() }).ToArray() : null,
                bcc = !string.IsNullOrEmpty(bcc) ? bcc.Split(',', ';').Select(e => new { email = e.Trim() }).ToArray() : null,
                subject,
                htmlContent = isHtml ? body : null,
                textContent = !isHtml ? body : null
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_options.BaseUrl}/smtp/email")
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(payload),
                    Encoding.UTF8,
                    "application/json")
            };
            request.Headers.Add("api-key", _options.ApiKey);

            var response = await _retryPolicy.ExecuteAsync(() => _httpClient.SendAsync(request, cancellationToken));

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
                var doc = JsonDocument.Parse(responseContent);
                var messageId = doc.RootElement.GetProperty("messageId").GetString();
                _logger.LogInformation("Email sent successfully to {To}. MessageId: {MessageId}", to, messageId);
                return EmailSendResult.Success(messageId);
            }

            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError("Failed to send email to {To}. Status: {Status}, Error: {Error}",
                to, response.StatusCode, error);
            return EmailSendResult.Failure($"HTTP {(int)response.StatusCode}: {error}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception sending email to {To}", to);
            return EmailSendResult.Failure(ex.Message);
        }
    }
}
