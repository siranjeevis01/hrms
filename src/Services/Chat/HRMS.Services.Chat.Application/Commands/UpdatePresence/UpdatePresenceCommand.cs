using HRMS.Services.Chat.Domain.Enums;
using MediatR;

namespace HRMS.Services.Chat.Application.Commands.UpdatePresence;

public class UpdatePresenceCommand : IRequest
{
    public Guid EmployeeId { get; set; }
    public PresenceStatus Status { get; set; }
    public Guid TenantId { get; set; }
}
