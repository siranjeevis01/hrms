using MediatR;

namespace HRMS.Services.Organization.Application.Events;

public record BranchCreatedEvent : INotification
{
    public Guid BranchId { get; init; }
    public Guid CompanyId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public bool IsHeadquarters { get; init; }
    public Guid TenantId { get; init; }
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}
