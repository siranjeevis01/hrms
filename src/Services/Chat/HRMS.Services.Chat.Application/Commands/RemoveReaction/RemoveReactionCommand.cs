using MediatR;

namespace HRMS.Services.Chat.Application.Commands.RemoveReaction;

public class RemoveReactionCommand : IRequest
{
    public Guid MessageId { get; set; }
    public Guid EmployeeId { get; set; }
    public string Emoji { get; set; } = string.Empty;
}
