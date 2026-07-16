using MediatR;

namespace HRMS.Services.Chat.Application.Commands.AddReaction;

public class AddReactionCommand : IRequest
{
    public Guid MessageId { get; set; }
    public Guid EmployeeId { get; set; }
    public string Emoji { get; set; } = string.Empty;
    public Guid TenantId { get; set; }
}
