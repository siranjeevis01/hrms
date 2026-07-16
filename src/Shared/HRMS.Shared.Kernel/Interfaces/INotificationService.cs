namespace HRMS.Shared.Kernel.Interfaces;

public interface INotificationService
{
    Task SendEmailAsync(string to, string subject, string body, bool isHtml = false, CancellationToken cancellationToken = default);
    Task SendEmailAsync(IReadOnlyList<string> to, string subject, string body, bool isHtml = false, CancellationToken cancellationToken = default);
    Task SendPushNotificationAsync(Guid userId, string title, string body, Dictionary<string, string>? data = null, CancellationToken cancellationToken = default);
    Task SendInAppNotificationAsync(Guid userId, string title, string message, string? actionUrl = null, CancellationToken cancellationToken = default);
}
