namespace HRMS.Services.Notification.Application.Interfaces;

public interface IPushProvider
{
    Task<PushSendResult> SendAsync(
        string userId, string title, string body, Dictionary<string, string>? data = null,
        CancellationToken cancellationToken = default);
}

public class PushSendResult
{
    public bool IsSuccess { get; set; }
    public string? ProviderMessageId { get; set; }
    public string? ErrorMessage { get; set; }

    public static PushSendResult Success(string? messageId = null) =>
        new() { IsSuccess = true, ProviderMessageId = messageId };

    public static PushSendResult Failure(string error) =>
        new() { IsSuccess = false, ErrorMessage = error };
}
