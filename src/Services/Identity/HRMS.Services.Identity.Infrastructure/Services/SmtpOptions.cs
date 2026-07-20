namespace HRMS.Services.Identity.Infrastructure.Services;

public sealed class SmtpOptions
{
    public const string SectionName = "Smtp";
    public string Host { get; set; } = "localhost";
    public int Port { get; set; } = 587;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FromEmail { get; set; } = "noreply@hrmspro.com";
    public string FromName { get; set; } = "HRMS Pro";
    public bool EnableSsl { get; set; } = true;
}
