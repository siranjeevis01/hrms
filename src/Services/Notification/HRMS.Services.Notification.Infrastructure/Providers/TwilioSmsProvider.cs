using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using HRMS.Services.Notification.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;

namespace HRMS.Services.Notification.Infrastructure.Providers;

public class TwilioSmsOptions
{
    public string AccountSid { get; set; } = string.Empty;
    public string AuthToken { get; set; } = string.Empty;
    public string FromNumber { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = "https://api.twilio.com/2010-04-01";
}

public class TwilioSmsProvider : ISmsProvider
{
    private readonly HttpClient _httpClient;
    private readonly TwilioSmsOptions _options;
    private readonly ILogger<TwilioSmsProvider> _logger;
    private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;

    public TwilioSmsProvider(
        HttpClient httpClient,
        IOptions<TwilioSmsOptions> options,
        ILogger<TwilioSmsProvider> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;

        var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_options.AccountSid}:{_options.AuthToken}"));
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

        _retryPolicy = Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                onRetry: (outcome, delay, attempt, context) =>
                {
                    _logger.LogWarning("Retry {Attempt} for SMS after {Delay}s. Status: {Status}",
                        attempt, delay.TotalSeconds, outcome.Result?.StatusCode);
                });
    }

    public async Task<SmsSendResult> SendAsync(
        string phoneNumber, string message,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var payload = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "To", phoneNumber },
                { "From", _options.FromNumber },
                { "Body", message }
            });

            var url = $"{_options.BaseUrl}/Accounts/{_options.AccountSid}/Messages.json";
            var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = payload };

            var response = await _retryPolicy.ExecuteAsync(() => _httpClient.SendAsync(request, cancellationToken));

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
                var doc = JsonDocument.Parse(responseContent);
                var sid = doc.RootElement.GetProperty("sid").GetString();
                _logger.LogInformation("SMS sent successfully to {Phone}. SID: {Sid}", phoneNumber, sid);
                return SmsSendResult.Success(sid);
            }

            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError("Failed to send SMS to {Phone}. Status: {Status}, Error: {Error}",
                phoneNumber, response.StatusCode, error);
            return SmsSendResult.Failure($"HTTP {(int)response.StatusCode}: {error}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception sending SMS to {Phone}", phoneNumber);
            return SmsSendResult.Failure(ex.Message);
        }
    }
}
