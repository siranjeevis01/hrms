using MediatR;

namespace HRMS.Services.Chat.Application.Commands.UpdateConversation;

public class UpdateConversationCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? IsPrivate { get; set; }
}
