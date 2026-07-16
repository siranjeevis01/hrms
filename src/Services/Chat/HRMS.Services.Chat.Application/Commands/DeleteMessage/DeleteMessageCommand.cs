using MediatR;

namespace HRMS.Services.Chat.Application.Commands.DeleteMessage;

public class DeleteMessageCommand : IRequest
{
    public Guid MessageId { get; set; }
}
