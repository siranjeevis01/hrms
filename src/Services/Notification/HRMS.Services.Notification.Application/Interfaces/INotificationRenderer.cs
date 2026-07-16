namespace HRMS.Services.Notification.Application.Interfaces;

public interface INotificationRenderer
{
    string Render(string template, Dictionary<string, string> variables);
    string? RenderSubject(string? subject, Dictionary<string, string> variables);
}
