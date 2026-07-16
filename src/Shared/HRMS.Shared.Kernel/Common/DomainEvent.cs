using MediatR;

namespace HRMS.Shared.Kernel.Common;

public abstract class DomainEvent : INotification
{
    public Guid Id { get; }
    public DateTime OccurredOn { get; }
    public string EventType { get; }

    protected DomainEvent(string eventType)
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
        EventType = eventType;
    }
}
