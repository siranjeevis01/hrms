namespace HRMS.Services.Notification.Application.Interfaces;

public interface ISmsProvider
{
    Task<SmsSendResult> SendAsync(
        string phoneNumber, string message,
        CancellationToken cancellationToken = default);
}

public class SmsSendResult
{
    public bool IsSuccess { get; set; }
    public string? ProviderMessageId { get; set; }
    public string? ErrorMessage { get; set; }

    public static SmsSendResult Success(string? messageId = null) =>
        new() { IsSuccess = true, ProviderMessageId = messageId };

    public static SmsSendResult Failure(string error) =>
        new() { IsSuccess = false, ErrorMessage = error };
}
