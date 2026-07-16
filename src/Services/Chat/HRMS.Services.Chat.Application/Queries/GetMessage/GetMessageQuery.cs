using HRMS.Services.Chat.Application.DTOs;
using MediatR;

namespace HRMS.Services.Chat.Application.Queries.GetMessage;

public class GetMessageQuery : IRequest<MessageDto?>
{
    public Guid Id { get; set; }
}
