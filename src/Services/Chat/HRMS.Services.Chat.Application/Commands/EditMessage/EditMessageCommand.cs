using MediatR;

namespace HRMS.Services.Chat.Application.Commands.EditMessage;

public class EditMessageCommand : IRequest
{
    public Guid MessageId { get; set; }
    public string Content { get; set; } = string.Empty;
}
