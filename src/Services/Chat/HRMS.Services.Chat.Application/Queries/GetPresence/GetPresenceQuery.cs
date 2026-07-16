using HRMS.Services.Chat.Application.DTOs;
using MediatR;

namespace HRMS.Services.Chat.Application.Queries.GetPresence;

public class GetPresenceQuery : IRequest<UserPresenceDto?>
{
    public Guid EmployeeId { get; set; }
}
