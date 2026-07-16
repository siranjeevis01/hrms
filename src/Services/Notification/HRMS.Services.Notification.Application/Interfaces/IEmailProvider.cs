namespace HRMS.Services.Notification.Application.Interfaces;

public interface IEmailProvider
{
    Task<EmailSendResult> SendAsync(
        string to, string subject, string body, bool isHtml,
        string? cc = null, string? bcc = null, string? attachments = null,
        CancellationToken cancellationToken = default);
}

public class EmailSendResult
{
    public bool IsSuccess { get; set; }
    public string? ProviderMessageId { get; set; }
    public string? ErrorMessage { get; set; }

    public static EmailSendResult Success(string? messageId = null) =>
        new() { IsSuccess = true, ProviderMessageId = messageId };

    public static EmailSendResult Failure(string error) =>
        new() { IsSuccess = false, ErrorMessage = error };
}
