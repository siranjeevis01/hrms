using HRMS.Services.Performance.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Performance.Domain.Entities;

public class CalibrationSession : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string ReviewPeriod { get; private set; } = string.Empty;
    public Guid ConductedBy { get; private set; }
    public CalibrationStatus Status { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private readonly List<CalibrationEntry> _entries = new();
    public IReadOnlyCollection<CalibrationEntry> Entries => _entries.AsReadOnly();

    private CalibrationSession() { }

    public static CalibrationSession Create(
        string name,
        string? description,
        string reviewPeriod,
        Guid conductedBy,
        string tenantId)
    {
        return new CalibrationSession
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            ReviewPeriod = reviewPeriod,
            ConductedBy = conductedBy,
            Status = CalibrationStatus.Draft,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Start()
    {
        Status = CalibrationStatus.InProgress;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        Status = CalibrationStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }

    internal void AddEntry(CalibrationEntry entry)
    {
        _entries.Add(entry);
    }
}
