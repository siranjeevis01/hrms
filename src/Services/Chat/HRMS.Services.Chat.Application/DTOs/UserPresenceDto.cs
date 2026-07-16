using HRMS.Services.Chat.Domain.Enums;

namespace HRMS.Services.Chat.Application.DTOs;

public class UserPresenceDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public PresenceStatus PresenceStatus { get; set; }
    public DateTime? LastSeenAt { get; set; }
    public Guid TenantId { get; set; }
}
