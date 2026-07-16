using MediatR;

namespace HRMS.Services.Organization.Application.Events;

public record DepartmentCreatedEvent : INotification
{
    public Guid DepartmentId { get; init; }
    public Guid CompanyId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public Guid? ParentDepartmentId { get; init; }
    public Guid TenantId { get; init; }
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}
