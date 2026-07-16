namespace HRMS.Services.Identity.Application.DTOs;

public class UserSessionDto
{
    public Guid Id { get; set; }
    public string DeviceInfo { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public DateTime LastActiveAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsCurrent { get; set; }
}
