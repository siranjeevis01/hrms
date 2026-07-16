using HRMS.Services.Notification.Application.Interfaces;

namespace HRMS.Services.Notification.Infrastructure.Services;

public class NotificationRenderer : INotificationRenderer
{
    public string Render(string template, Dictionary<string, string> variables)
    {
        if (string.IsNullOrEmpty(template) || variables == null || !variables.Any())
            return template;

        var result = template;
        foreach (var kvp in variables)
        {
            result = result.Replace($"{{{{{kvp.Key}}}}}", kvp.Value);
        }
        return result;
    }

    public string? RenderSubject(string? subject, Dictionary<string, string> variables)
    {
        if (string.IsNullOrEmpty(subject) || variables == null || !variables.Any())
            return subject;

        var result = subject;
        foreach (var kvp in variables)
        {
            result = result.Replace($"{{{{{kvp.Key}}}}}", kvp.Value);
        }
        return result;
    }
}
