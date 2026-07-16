using MediatR;

namespace HRMS.Services.Chat.Application.Events;

public class ParticipantAddedEvent : INotification
{
    public Guid ConversationId { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid AddedBy { get; set; }
    public DateTime AddedAt { get; set; }
}
