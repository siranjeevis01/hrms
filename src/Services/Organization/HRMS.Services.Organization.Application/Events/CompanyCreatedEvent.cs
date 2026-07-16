using MediatR;

namespace HRMS.Services.Organization.Application.Events;

public record CompanyCreatedEvent : INotification
{
    public Guid CompanyId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string LegalName { get; init; } = string.Empty;
    public Guid TenantId { get; init; }
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}
