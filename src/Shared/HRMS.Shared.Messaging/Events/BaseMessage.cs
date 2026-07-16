namespace HRMS.Shared.Messaging.Events;

public abstract class BaseMessage
{
    public Guid Id { get; init; }
    public DateTime Timestamp { get; init; }
    public string EventType { get; init; }
    public string? CorrelationId { get; init; }
    public Guid? TenantId { get; init; }

    protected BaseMessage(string eventType)
    {
        Id = Guid.NewGuid();
        Timestamp = DateTime.UtcNow;
        EventType = eventType;
    }
}
