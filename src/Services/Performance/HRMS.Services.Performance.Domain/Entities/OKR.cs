using HRMS.Services.Performance.Domain.Enums;
using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Performance.Domain.Entities;

public class OKR : AggregateRoot
{
    public Guid EmployeeId { get; private set; }
    public Guid? ManagerId { get; private set; }
    public string Period { get; private set; } = string.Empty;
    public new OKRStatus Status { get; private set; }
    public decimal? OverallScore { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private readonly List<OKRItem> _items = new();
    public IReadOnlyCollection<OKRItem> Items => _items.AsReadOnly();

    private OKR() { }

    public static OKR Create(
        Guid employeeId,
        Guid? managerId,
        string period,
        string tenantId)
    {
        return new OKR
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            ManagerId = managerId,
            Period = period,
            Status = OKRStatus.Draft,
            OverallScore = 0,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(Guid? managerId, string? period)
    {
        ManagerId = managerId ?? ManagerId;
        Period = period ?? Period;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Submit()
    {
        Status = OKRStatus.Submitted;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Approve(decimal? overallScore)
    {
        Status = OKRStatus.Approved;
        OverallScore = overallScore ?? OverallScore;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reject()
    {
        Status = OKRStatus.Rejected;
        UpdatedAt = DateTime.UtcNow;
    }

    internal void AddItem(OKRItem item)
    {
        _items.Add(item);
    }

    public new void RaiseEvent(INotification domainEvent)
    {
        base.RaiseEvent(domainEvent);
    }
}
